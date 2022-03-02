
using EDIWarehouseIN;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfICTGeTech.Interface;
using WcfICTGeTech.Model;
using static EDIWarehouseIN.WCF.CommonModel;

namespace WcfICTGeTech.Service.OperationAdapter
{
    public class ATSStockInCheckAdapter : AbstractDataAdapter<ATSStockInCheckInModel, ATSStockInCheckOutModel>
    {
        private FGINRETURNMODEL mesInfo { get; set; }
        //private MESReturnModel mesInfo { get; set; }

        public ATSStockInCheckAdapter(string data)
            :base(data) {
            this.taskNo = inModel.TaskNo;
        }
        EDIWarehouseINBLL wb = new EDIWarehouseINBLL();
        //protected override string callAction { get { return "ATSStockInCheck"; } set { } }

        protected override object CheckModel()
        {
            MESGateway mes = new MESGateway();
            var MesSnModel = mes.GetMESStockInfo(inModel.PalletNo);
            //if (mesInfo.SNLIST == null || mesInfo.SNLIST.Count() < 1)
            //    throw new Exception("找不到 SN");
            //var palletNo = mesInfo.SNLIST.Select(x => x.PALLET_NO).Distinct();
            //if (palletNo.Count() != 1)
            //    throw new Exception("找到多个 PalletNo");
            //if(palletNo.FirstOrDefault() != inModel.PalletNo)
            //    throw new Exception("传入PalletNo 与 MES 不符合");
            // 待补充SN检查是否重复
            string strINSN = MesSnModel.INSN;
            string strResultModel = MesSnModel.RESULT;
            string strErrmsg = MesSnModel.MSG;
            SNLIST[] TeturnSNList = MesSnModel.SNLIST;
            string strGUID = System.Guid.NewGuid().ToString().ToUpper();
            for (int i = 0; i < TeturnSNList.Count(); i++)
            {
                SNLIST sninfo = TeturnSNList[i];
                //insert
                //SP内检查PPS是否序号重复， 如果OK，
                string strerrmsg = string.Empty;
                string strResult = wb.ExecuteFGIN(strGUID, sninfo, "WATCH", "Y", out strerrmsg);
                if (!strResult.Equals("OK"))
                {
                    throw new Exception(strerrmsg);
                }
            }
            return mesInfo;
        }

        protected override ATSStockInCheckOutModel DoAction(object bMsg)
        {
            //var palletNo = mesInfo.SNLIST.Select(x => x.).FirstOrDefault();
            //var ATSPalletNo = mesInfo.SNLIST.Select(x => x.ATSPalletNo).FirstOrDefault();
            //ATSStockInCheckOutModel ret = new ATSStockInCheckOutModel
            //{
            //    Msg = "",
            //    Result=true,
            //    PalletNo = palletNo,
            //    TaskNo = inModel.TaskNo,
            //    ATSPalletNo = ATSPalletNo,
            //    SNList = mesInfo.SNLIST.Select(x => new MESSNList
            //    {
            //        BUCKETID = x.BUCKETID,
            //        SN = x.SN,
            //        CARTONID = x.CARTONID,
            //        PART_DESC = x.PART_DESC,
            //        PART_NO = x.PART_NO,
            //    }).ToList()
            //};

            var palletNo = mesInfo.SNLIST.Select(x => x.PALLETID).FirstOrDefault();
            var ATSPalletNo = mesInfo.SNLIST.Select(x => x.PALLETID).FirstOrDefault();
            ATSStockInCheckOutModel ret = new ATSStockInCheckOutModel
            {
                Msg = "",
                Result = true,
                PalletNo = palletNo,
                TaskNo = inModel.TaskNo,
                ATSPalletNo = ATSPalletNo,
                SNList = mesInfo.SNLIST.Select(x => new MESSNList
                {
                    //BUCKETID = x.BUCKETID,
                    SN = x.SN,
                    CARTONID = x.BOXID,
                    //PART_DESC = x.PART_DESC,
                    PART_NO = x.PN,
                }).ToList()
            };
            return ret;
        }


    }
}
