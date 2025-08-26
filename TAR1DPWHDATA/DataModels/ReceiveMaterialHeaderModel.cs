using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TAR1DPWHDATA.DataModels
{
    public class ReceiveMaterialHeaderModel
    {
        public int Id { get; set; }
        public string ReceivedDate { get; set; }
        public int PreparedById { get; set; }
        public string PreparedBy { get; set; }
        public string PosPrepBy { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double ReceivedTotalCost { get; set; }
        public int ReceivedById { get; set; }
        public string ReceivedBy { get; set; }
        public string PosRecBy { get; set; }
        public int CheckedById { get; set; }
        public string CheckedBy { get; set; }
        public string PosChckBy { get; set; }
        public int NotedById { get; set; }
        public string NotedBy { get; set; }
        public string PosNoteBy { get; set; }
        public int AuditedById { get; set; }
        public string AuditedBy { get; set; }
        public string PosAudBy { get; set; }
        public bool IsOld { get; set; }
        public int? SupplierId { get; set; }
        public string Supplier { get; set; }
        public string PO1 { get; set; }
        public string PO2 { get; set; }
        public string PO3 { get; set; }
        public string PO4 { get; set; }
        public string PO5 { get; set; }
        public string POs { get; set; }
        public string SI1 { get; set; }
        public string SI2 { get; set; }
        public string SI3 { get; set; }
        public string SI4 { get; set; }
        public string SI5 { get; set; }
        public string SIs { get; set; }
        public string DR1 { get; set; }
        public string DR2 { get; set; }
        public string DR3 { get; set; }
        public string DR4 { get; set; }
        public string DR5 { get; set; }
        public string DRs { get; set; }
        public string DeliveryDate { get; set; }
        public string Remark { get; set; }
    }
}