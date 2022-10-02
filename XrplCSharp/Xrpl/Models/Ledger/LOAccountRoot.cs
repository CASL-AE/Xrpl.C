﻿using Newtonsoft.Json;

using System;

using Xrpl.ClientLib.Json.Converters;
using Xrpl.Models.Common;

// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/src/models/ledger/AccountRoot.ts

namespace Xrpl.Models.Ledger
{
    [Flags]
    public enum AccountRootFlags : uint
    {
        /// <summary>
        /// The account has used its free SetRegularKey transaction.
        /// </summary>
        lsfPasswordSpent = 65536,
        /// <summary>
        /// Requires incoming payments to specify a Destination Tag.
        /// </summary>
        lsfRequireDestTag = 131072,
        /// <summary>
        /// This account must individually approve other users for those users to hold this account's issued currencies.
        /// </summary>
        lsfRequireAuth = 262144,
        /// <summary>
        ///  Client applications should not send XRP to this account. Not enforced by rippled.
        /// </summary>
        lsfDisallowXRP = 524288,
        /// <summary>
        /// Disallows use of the master key to sign transactions for this account.
        /// </summary>
        lsfDisableMaster = 1048576,
        /// <summary>
        /// This address cannot freeze trust lines connected to it. Once enabled, cannot be disabled.
        /// </summary>
        lsfNoFreeze = 2097152,
        /// <summary>
        /// All assets issued by this address are frozen.
        /// </summary>
        lsfGlobalFreeze = 4194304,
        /// <summary>
        /// Enable rippling on this addresses's trust lines by default. Required for issuing addresses; discouraged for others.
        /// </summary>
        lsfDefaultRipple = 8388608,
        //todo not found field   lsfDepositAuth = 0x01000000 
        //This account can only receive funds from transactions it sends, and from preauthorized accounts.
        //(It has DepositAuth enabled.)

    }
    /// <summary>
    /// The AccountRoot object type describes a single account, its settings, and XRP balance.
    /// </summary>
    public class LOAccountRoot : BaseLedgerEntry
    {
        public LOAccountRoot()
        {
            LedgerEntryType = LedgerEntryType.AccountRoot;
        }
        /// <summary>
        /// The identifying (classic) address of this account.
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// A bit-map of boolean flags enabled for this account.
        /// </summary>
        public AccountRootFlags Flags { get; set; }
        /// <summary>
        /// The sequence number of the next valid transaction for this account.
        /// </summary>
        public uint Sequence { get; set; }
        /// <summary>
        /// The account's current XRP balance in drops, represented as a string.
        /// </summary>
        [JsonConverter(typeof(CurrencyConverter))]
        public Currency Balance { get; set; }
        /// <summary>
        /// The number of objects this account owns in the ledger, which contributes to its owner reserve.
        /// </summary>
        public uint OwnerCount { get; set; }
        /// <summary>
        /// The identifying hash of the transaction that most recently modified this object.
        /// </summary>
        public string PreviousTxnID { get; set; }
        /// <summary>
        /// The index of the ledger that contains the transaction that most recently modified this object.
        /// </summary>
        public uint PreviousTxnLgrSeq { get; set; }
        /// <summary>
        /// The identifying hash of the transaction most recently sent by this account.<br/>
        /// This field must be enabled to use the AccountTxnID transaction field.<br/>
        /// To enable it, send an AccountSet transaction with the.<br/>
        /// `asfAccountTxnID` flag enabled.
        /// </summary>
        public string AccountTxnID { get; set; }
        /// <summary>
        /// The address of a key pair that can be used to sign transactions for this account instead of the master key.<br/>
        /// Use a SetRegularKey transaction to change this value.
        /// </summary>
        public string RegularKey { get; set; }
        /// <summary>
        /// The md5 hash of an email address.
        /// </summary>
        public string EmailHash { get; set; }
        /// <summary>
        /// A public key that may be used to send encrypted messages to this account in JSON, uses hexadecimal.
        /// </summary>
        public string MessageKey { get; set; }
        /// <summary>
        /// How many significant digits to use for exchange rates of Offers involving currencies issued by this address.<br/>
        /// Valid values are 3 to 15, inclusive.
        /// </summary>
        public byte? TickSize { get; set; }
        /// <summary>
        /// A transfer fee to charge other users for sending currency issued by this account to each other.
        /// </summary>
        public uint? TransferRate { get; set; }
        /// <summary>
        ///  A domain associated with this account.<br/>
        /// In JSON, this is the hexadecimal for the ASCII representation of the domain.
        /// </summary>
        public string Domain { get; set; }


        //todo not found field number - TicketCount?
        // How many Tickets this account owns in the ledger.<br/>
        // This is updated automatically to ensure that the account stays within the hard limit of 250.<br/>
        // Tickets at a time. 


    }
}
