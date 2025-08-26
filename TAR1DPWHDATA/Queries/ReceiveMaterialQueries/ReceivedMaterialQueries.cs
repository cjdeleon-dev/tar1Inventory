using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAR1DPWHDATA.Queries.ReceiveMaterialQueries
{
    public class ReceivedMaterialQueries
    {
        public static readonly string sqlGetAllReceivedMaterialHeaders = "select rm.id,receiveddate,preparedbyid,UPPER(us.firstname + ' ' + us.middleinitial + ' ' + us.lastname) [PreparedBy], posprep.Description [PosPrepBy], " +
                                                                         "receivedtotalcost, IsOld, empr.Name [ReceivedBy], posrec.Description [PosRecBy], " + 
                                                                         "empc.Name [CheckedBy], posc.Description [PosChckBy], empn.Name [NotedBy], posn.Description [PosNoteBy], empa.Name [AuditedBy], posa.Description [PosAudBy], " +
                                                                         "sup.Name [Supplier],ISNULL(PO1,'') PO1, ISNULL(PO2,'') PO2,ISNULL(PO3,'') PO3,ISNULL(PO4,'') PO4,ISNULL(PO5,'') PO5, " +
                                                                         "ISNULL(SI1, '') SI1, ISNULL(SI2,'') SI2,ISNULL(SI3,'') SI3,ISNULL(SI4,'') SI4,ISNULL(SI5,'') SI5, " +
                                                                         "ISNULL(DR1, '') DR1, ISNULL(DR2,'') DR2,ISNULL(DR3,'') DR3,ISNULL(DR4,'') DR4,ISNULL(DR5,'') DR5, Remark, CONVERT(varchar(10),DeliveryDate,101) DeliveryDate " +
                                                                         "from receivedmaterialheaders rm inner join users us " +
                                                                         "on rm.preparedById=us.Id " +
                                                                         "left join Positions posprep " +
                                                                         "on us.PositionId=posprep.Id " +
                                                                         "left join Employees empr " +
                                                                         "on rm.ReceivedById=empr.Id " +
                                                                         "left join Positions posrec " +
                                                                         "on empr.PositionId=posrec.Id " +
                                                                         "left join Employees empc " +
                                                                         "on rm.CheckedById=empc.Id " +
                                                                         "left join Positions posc " +
                                                                         "on empc.PositionId=posc.Id " +
                                                                         "left join Employees empn " +
                                                                         "on rm.NotedById=empn.Id " +
                                                                         "left join Positions posn " +
                                                                         "on empn.PositionId=posn.Id " +
                                                                         "left join Employees empa " +
                                                                         "on rm.AuditedById=empa.Id " +
                                                                         "left join Positions posa " +
                                                                         "on empa.PositionId=posa.Id " +
                                                                         "left join Suppliers sup " +
                                                                         "on rm.SupplierId=sup.Id " +
                                                                         "order by rm.id desc;";

        public static readonly string sqlInsertReceivedMaterialHeader = "insert into receivedmaterialheaders(receiveddate,preparedbyid,receivedtotalcost," +
                                                                        "createdbyid,receivedbyid,checkedbyid,notedbyid,auditedbyid,isold," +
                                                                        "supplierid,po1,po2,po3,po4,po5,si1,si2,si3,si4,si5," +
                                                                        "dr1,dr2,dr3,dr4,dr5,remark,deliverydate) " +
                                                                        "values(@receiveddate,@preparedbyid,@receivedtotalcost,@preparedbyid," +
                                                                        " case when @receivedbyid=0 then null else @receivedbyid end," +
                                                                        " case when @checkedbyid=0 then null else @checkedbyid end," +
                                                                        " case when @notedbyid=0 then null else @notedbyid end," +
                                                                        " case when @auditedbyid=0 then null else @auditedbyid end," +
                                                                        "@isold," +
                                                                        "case when @supplierid=0 then null else @supplierid end," +
                                                                        "@po1,@po2,@po3,@po4,@po5,@si1,@si2,@si3,@si4,@si5," +
                                                                        "@dr1,@dr2,@dr3,@dr4,@dr5,@remark,@deliverydate);";

        public static readonly string sqlGetCurrentRMIdByUserId = "select max(Id) Id from receivedmaterialheaders where preparedbyid=@preparedbyid;";

        public static readonly string sqlInsertReceivedMaterialDetail = "insert into ReceivedMaterialDetails(RecievedMaterialHeaderId,MaterialId,Quantity,UnitId,UnitCost,TotalCost,InventorialCost,VAT,OnHand,BalanceQty,Remark) " +
                                                                        "values(@RecievedMaterialHeaderId,@MaterialId,@Quantity,@UnitId,@UnitCost,@TotalCost,@InventorialCost,@VAT,@OnHand,@BalanceQty,@Remark); " +
                                                                        "update materials set OnHand=OnHand+@Quantity where id=@MaterialId;" +
                                                                        "update t " +
                                                                        "set t.ReceivedTotalCost=s.receivedtotalcost " +
                                                                        "from ReceivedMaterialHeaders t inner join " +
                                                                        "( " +
                                                                        " select RecievedMaterialHeaderId, sum(totalcost) receivedtotalcost " +                                       
                                                                        " from ReceivedMaterialDetails " +
                                                                        " where RecievedMaterialHeaderId = @RecievedMaterialHeaderId " +
                                                                        " group by RecievedMaterialHeaderId " +
                                                                        ") s on t.Id=s.RecievedMaterialHeaderId;";

        public static readonly string sqlGetRecMaterialDetailByHeaderId = "select dtl.id, recievedmaterialheaderid, materialid,mat.Description [material],dtl.quantity ,dtl.unitid,unt.code [unit],unitcost,totalcost,inventorialcost,vat,dtl.onhand " +
                                                                          "from receivedmaterialdetails dtl inner join materials mat on dtl.materialid=mat.id " +
                                                                          "inner join units unt on dtl.unitid=unt.id " +
                                                                          "where dtl.recievedmaterialheaderid=@rmhdrid;";

        public static readonly string sqlGetEmployees = "select employees.id, employees.name, isnull(employees.positionid,0) positionid, isnull(positions.description,'') position from employees left join positions on employees.positionid=positions.id order by employees.name; ";

        public static readonly string sqlGetAllReceivedNonStockMaterialHeaders = "select id [Id], ReceivedDate, PreparedById, PreparedBy, ReceivedTotalCost,ReceivedBy,NotedBy,AuditedBy, Supplier, " +
                                                                         "REVERSE(SUBSTRING(REVERSE(SUBSTRING(POs, PATINDEX('%[^,]%', POs),99999)), PATINDEX('%[^,]%', REVERSE(SUBSTRING(POs, PATINDEX('%[^,]%', POs),99999))),99999)) [POs], " +
                                                                         "REVERSE(SUBSTRING(REVERSE(SUBSTRING(SIs, PATINDEX('%[^,]%', SIs),99999)), PATINDEX('%[^,]%', REVERSE(SUBSTRING(SIs, PATINDEX('%[^,]%', SIs),99999))),99999)) [SIs], " +
                                                                         "REVERSE(SUBSTRING(REVERSE(SUBSTRING(DRs, PATINDEX('%[^,]%', DRs),99999)), PATINDEX('%[^,]%', REVERSE(SUBSTRING(DRs, PATINDEX('%[^,]%', DRs),99999))),99999)) [DRs] " +
                                                                         "from " +
                                                                         "( " +
                                                                         "	select rm.id,receiveddate,preparedbyid,UPPER(us.firstname + ' ' + us.middleinitial + ' ' + us.lastname) [PreparedBy], " +
                                                                         "	receivedtotalcost, empr.Name [ReceivedBy], empn.Name [NotedBy], empa.Name [AuditedBy], " +
                                                                         "	sup.Name [Supplier],REPLACE(PO1,0,'')+','+REPLACE(PO2,0,'')+','+REPLACE(PO3,0,'')+','+REPLACE(PO4,0,'')+','+REPLACE(PO5,0,'') [POs], " +
                                                                         "	REPLACE(SI1,0,'')+','+REPLACE(SI2,0,'')+','+REPLACE(SI3,0,'')+','+REPLACE(SI4,0,'')+','+REPLACE(SI5,0,'') [SIs], " +
                                                                         "	REPLACE(DR1,0,'')+','+REPLACE(DR2,0,'')+','+REPLACE(DR3,0,'')+','+REPLACE(DR4,0,'')+','+REPLACE(DR5,0,'') [DRs] " +
                                                                         "	from receivednonstockheaders rm inner join users us " +
                                                                         "	on rm.preparedById=us.Id " +
                                                                         "	inner join Employees empr " +
                                                                         "	on rm.ReceivedById=empr.Id " +
                                                                         "	inner join Employees empn " +
                                                                         "	on rm.NotedById=empn.Id " +
                                                                         "	inner join Employees empa " +
                                                                         "	on rm.AuditedById=empa.Id " +
                                                                         "	inner join Suppliers sup " +
                                                                         "	on rm.SupplierId=sup.Id " +
                                                                         ") hdr " +
                                                                         "order by hdr.id desc;";

        public static readonly string sqlInsertReceivedNonStockHeader = "insert into receivednonstockheaders(receiveddate,preparedbyid,receivedtotalcost," +
                                                                        "createdbyid,receivedbyid,notedbyid,auditedbyid," +
                                                                        "supplierid,po1,po2,po3,po4,po5,si1,si2,si3,si4,si5," +
                                                                        "dr1,dr2,dr3,dr4,dr5) " +
                                                                        "values(@receiveddate,@preparedbyid,@receivedtotalcost,@preparedbyid," +
                                                                        " case when @receivedbyid=0 then null else @receivedbyid end," +
                                                                        " case when @notedbyid=0 then null else @notedbyid end," +
                                                                        " case when @auditedbyid=0 then null else @auditedbyid end," +
                                                                        "@supplierid,@po1,@po2,@po3,@po4,@po5,@si1,@si2,@si3,@si4,@si5," +
                                                                        "@dr1,@dr2,@dr3,@dr4,@dr5);";

        public static readonly string sqlGetCurrentRMNSIdByUserId = "select max(Id) Id from receivednonstockheaders where preparedbyid=@preparedbyid;";

        public static readonly string sqlInsertReceivedNonStockDetail = "insert into ReceivedNonStockDetails(ReceivedNonStockHeaderId,NonStockId,Quantity,UnitId,UnitCost,TotalCost,InventorialCost,VAT,OnHand) " +
                                                                        "values(@ReceivedNonStockHeaderId,@NonStockId,@Quantity,@UnitId,@UnitCost,@TotalCost,@InventorialCost,@VAT,@OnHand); " +
                                                                        "update nonstocks set OnHand=OnHand+@Quantity where id=@NonStockId;";

        public static readonly string sqlGetRecNonStockDetailByHeaderId = "select dtl.id, receivednonstockheaderid, nonstockid,ns.Description [material],dtl.quantity ,dtl.unitid,unt.Description [unit],unitcost,totalcost,inventorialcost,vat,dtl.onhand " +
                                                                          "from receivednonstockdetails dtl inner join nonstocks ns on dtl.nonstockid=ns.id " +
                                                                          "inner join units unt on dtl.unitid=unt.id " +
                                                                          "where dtl.receivednonstockheaderid=@rmhdrid;";

        public static readonly string sqlGetRecMaterialBalanceDetailByHeaderid = "select rmbd.Id,RecievedMaterialHeaderId,rmbd.MaterialId,mat.Description [Material],BalanceQty [Quantity],UnitId,units.Code [Unit],Remark " +
                                                                                 "from ReceivedMaterialDetails rmbd inner join Materials mat " +
                                                                                 "on rmbd.MaterialId=mat.Id " +
                                                                                 "inner join units on rmbd.UnitId=units.Id " +
                                                                                 "where RecievedMaterialHeaderId=@rmhid and (BalanceQty>0 or rtrim(remark)<>'');";
    }
}