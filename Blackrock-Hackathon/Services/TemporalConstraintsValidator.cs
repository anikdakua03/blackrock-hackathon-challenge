using Blackrock_Hackathon.Interfaces;
using BlackRock_Hackathon.DTOs;
using BlackRock_Hackathon.Models;

namespace BlackRock_Hackathon.Services;

public class TemporalConstraintsValidator : ITemporalConstraintsValidator
{
    public (List<Transaction> valids, List<InvalidTransaction> invalids) ValidateWithFilter(TransactionValidatorFilterRequest transactionValidatorFilterRequest)
    {
        if (transactionValidatorFilterRequest.Wage < 0)
        {
            throw new InvalidDataException("Wage cannot be negative.");
        }

        List<Transaction> valids = [];
        List<InvalidTransaction> invalids = [];

        // Sort transactions by date
        List<Transaction> transactions = [.. transactionValidatorFilterRequest.Transactions.OrderBy(t => t.Date)];

        // Validate Q periods
        List<QPeriod> qPeriods = transactionValidatorFilterRequest.Q
            .Where(q => q.Start <= q.End && q.Fixed >= 0)
            .OrderByDescending(q => q.Start)
            .ToList() ?? [];

        // Prepare P sweep-line events
        SortedDictionary<DateTime, double> pEvents = [];

        foreach (PPeriod p in transactionValidatorFilterRequest.P)
        {
            if (p.Start > p.End || p.Extra < 0)
            {
                continue;
            }

            if (pEvents.ContainsKey(p.Start) == false)
            {
                pEvents[p.Start] = 0;
            }

            pEvents[p.Start] += p.Extra;

            DateTime endMarker = p.End.AddSeconds(1);

            if (pEvents.ContainsKey(endMarker) == false)
            {
                pEvents[endMarker] = 0;
            }

            pEvents[endMarker] -= p.Extra;
        }

        double runningExtra = 0;

        SortedDictionary<DateTime, double>.Enumerator pEventEnumerator = pEvents.GetEnumerator();

        bool hasPEnded = !pEventEnumerator.MoveNext();

        foreach (Transaction t in transactions)
        {
            double updatedRemanent = t.Remanent;

            // Apply Q (override)
            foreach (QPeriod q in qPeriods)
            {
                if (t.Date >= q.Start && t.Date <= q.End)
                {
                    updatedRemanent = q.Fixed;
                    break;
                }
            }

            // Apply P (sweep)
            while (hasPEnded == false && pEventEnumerator.Current.Key <= t.Date)
            {
                runningExtra += pEventEnumerator.Current.Value;
                hasPEnded = !pEventEnumerator.MoveNext();
            }

            updatedRemanent += runningExtra;

            if (updatedRemanent < 0)
            {
                invalids.Add(new InvalidTransaction(t.Date, t.Amount, t.Ceiling, updatedRemanent, "Remanent became negative after applying periods"));

                continue;
            }

            // Validate K coverage (optional rule)
            if (transactionValidatorFilterRequest.K != null && transactionValidatorFilterRequest.K.Count > 0)
            {
                bool insideAnyK = transactionValidatorFilterRequest.K.Any(k =>
                    t.Date >= k.Start && t.Date <= k.End);

                if (insideAnyK == false)
                {
                    invalids.Add(new InvalidTransaction(t.Date, t.Amount, t.Ceiling, updatedRemanent, "Transaction outside all k periods"));

                    continue;
                }
            }

            // update the valid transaction only remananent
            valids.Add(t with { Remanent = updatedRemanent });
        }

        return (valids, invalids);
    }

}
