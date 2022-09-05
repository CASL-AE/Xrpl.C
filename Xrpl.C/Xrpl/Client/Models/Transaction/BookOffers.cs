﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Xrpl.Client.Json.Converters;
using Xrpl.Client.Models.Enums;
using Xrpl.Client.Models.Common;

namespace Xrpl.Client.Models.Transactions
{
    [Flags]
    public enum OfferFlags
    {
        lsfPassive = 65536,
        lsfSell = 131072
    }
    public class BookOffers
    {
        [JsonProperty("ledger_current_index")]
        public uint? LedgerCurrentIndex { get; set; }

        [JsonProperty("ledger_index")]
        public uint? LedgerIndex { get; set; }

        [JsonProperty("ledger_hash")]
        public string LedgerHash { get; set; }

        [JsonProperty("offers")]
        public List<Offer> Offers { get; set; }

    }

    public class Offer
    {
        public Offer()
        {
            LedgerEntryType = LedgerEntryType.Offer;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public LedgerEntryType LedgerEntryType { get; set; }

        [JsonProperty("index")]
        public string Index { get; set; }

        public string Account { get; set; }

        public decimal AmountEach
        {
            get
            {
                if ((TakerPays.ValueAsXrp ?? TakerPays.ValueAsNumber) != 0)
                {
                    return (TakerGets.ValueAsXrp ?? TakerGets.ValueAsNumber) /
                           (TakerPays.ValueAsXrp ?? TakerPays.ValueAsNumber);
                }
                return 0;
            }
        }

        public OfferFlags Flags { get; set; }

        public uint Sequence { get; set; }

        [JsonConverter(typeof(CurrencyConverter))]
        public Currency TakerPays { get; set; }

        [JsonConverter(typeof(CurrencyConverter))]
        public Currency TakerGets { get; set; }

        public string BookDirectory { get; set; }

        public string BookNode { get; set; }

        public string OwnerNode { get; set; }

        [JsonProperty("PreviousTxnID")]
        public string PreviousTxnID { get; set; }

        [JsonProperty("PreviousTxnLgrSeq")]
        public uint PreviousTxnLgrSeq { get; set; }

        [JsonConverter(typeof(RippleDateTimeConverter))]
        public DateTime? Expiration { get; set; }


        [JsonProperty("owner_funds")]
        public string OwnerFunds { get; set; }

        [JsonProperty("taker_gets_funded")]
        [JsonConverter(typeof(CurrencyConverter))]
        public Currency TakerGetsFunded { get; set; }

        [JsonProperty("taker_pays_funded")]
        [JsonConverter(typeof(CurrencyConverter))]
        public Currency TakerPaysFunded { get; set; }

        [JsonProperty("quality")]
        public double Quality { get; set; }
    }
}
