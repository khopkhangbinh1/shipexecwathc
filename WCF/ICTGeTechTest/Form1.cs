
using Newtonsoft.Json;
using OperationWCF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WcfICTGeTech.Interface;
using PPSSelfJobWcf;

namespace UnitTest
{
    public partial class Form1 : Form
    {
        private IGeTechToICT getWS()
        {
            return HttpChannel.Get<IGeTechToICT>("http://localhost:8101/ICTGetechWCF");
        }

        private IPPSSelfJobService getWS(string strurl)
        {
            return HttpChannel.Get<IPPSSelfJobService>(strurl);
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void btnATSStockInCheck_Click(object sender, EventArgs e)
        {
            try
            {
                var model = new
                {
                    TaskNo = Guid.NewGuid().ToString(),
                    PalletNo = "JSK20042515070088M"
                };
                var s = getWS().ATSStockInCheck(JsonConvert.SerializeObject(model));
                txtResult.Text = s;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnSendMailNotify_Click(object sender, EventArgs e)
        {
            try
            {
                var model = new
                {
                    Subject = "Mail Test",
                    Body = "Mail Body Test",
                    MailKey = "",
                    Emails = new string[] { "Joe.Pan@luxshare-ict.com" }
                };
                var s = getWS().SendMailNotify(JsonConvert.SerializeObject(model));
                txtResult.Text = s;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSync_Trolley_Click(object sender, EventArgs e)
        {
            try
            {
                var model = new
                {
                    items = Enumerable.Range(1, 10)
                        .Select(x => new
                        {
                            TROLLEY_ID = "TEST",
                            TROLLEY_TYPE = "GeTech1",
                            SIDES_NO = "A",
                            LEVEL_NO = x,
                            SEQ_NO = 1,
                            PACKQTY = 1,
                            MAXSEQ = 10,
                            ENABLED = "Y",
                            UPDATE_USERID = "100694",
                        }).ToList()
                };

                var s = getWS().Sync_Trolley(JsonConvert.SerializeObject(model));
                txtResult.Text = s;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSync_Location_Click(object sender, EventArgs e)
        {
            try
            {
                /*
			"ENABLED": ""                         //是否启用  Y or N
                 * */
                var model = new
                {
                    items = Enumerable.Range(1, 3)
                        .Select(x => new
                        {
                            // ATS=>立库 , TLY=>金刚车 , PWL=>拣选墙
                            LOCATION_NO = "TEST-JOE" + x,
                            LOCATION_TYPE = x == 1 ? "ATS" : x == 2 ? "TLY" : "PWL",
                            LOCATION_NAME = "TEST-JOE",
                            WAREHOUSE_NO = "成品仓",
                            ENABLED = "Y",
                        }).ToList()
                };

                var s = getWS().Sync_Location(JsonConvert.SerializeObject(model));
                txtResult.Text = s;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnATSStockIn_Click(object sender, EventArgs e)
        {
            try
            {
                var model = new
                {
                    SNList = new object[] {
                        new {
                           PART_NO = "PART_NO",
                           PART_DESC = "PART_DESC",
                           CARTONID = "CARTONID",
                           SN = "SN1",
                           BUCKETID = "BUCKETID",
                        },
                        new {
                           PART_NO = "PART_NO",
                           PART_DESC = "PART_DESC",
                           CARTONID = "CARTONID",
                           SN = "SN2",
                           BUCKETID = "BUCKETID",
                        }
                        ,
                        new {
                           PART_NO = "PART_NO2",
                           PART_DESC = "PART_DESC",
                           CARTONID = "CARTONID1",
                           SN = "SN3",
                           BUCKETID = "BUCKETID",
                        }
                    },
                    TaskNo = Guid.NewGuid().ToString(),
                    OPTYPE = "ATSStockIn",
                    PalletNo = "BOX000009",
                    ATSPalletNo = "",
                    ATSLocId = "Location"
                };
                var s = getWS().ATSStockIn(JsonConvert.SerializeObject(model));
                txtResult.Text = s;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPPSBOMReleaseResponse_Click(object sender, EventArgs e)
        {
            try
            {
                var model = new
                {
                    TaskNo = Guid.NewGuid().ToString(),
                    OPTYPE = "PPSBOMRelease",
                    AUFNR = "AUFNR",
                    AUART = "AUART",
                    CARTONS = new object[] {
                        new {
                            ICT_PARTNO = "ICT_PARTNO1",
                            CARTONID = "CARTONID1",
                            BUCKETID = "BUCKETID",
                            PalletNo = "PalletNo",
                            ATSPalletNo = "ATSPalletNo",
                            SN = new object[]{
                                new { SN = "SN11"},
                                new { SN = "SN12"},
                                new { SN = "SN13"},
                                new { SN = "SN14"}
                            }
                        },
                        new {
                            ICT_PARTNO = "ICT_PARTNO2",
                            CARTONID = "CARTONID2",
                            BUCKETID = "BUCKETID",
                            PalletNo = "PalletNo",
                            ATSPalletNo = "ATSPalletNo",
                            SN = new object[]{
                                new { SN = "SN21"}
                            }
                        },
                        new {
                            ICT_PARTNO = "ICT_PARTNO2",
                            CARTONID = "CARTONID3",
                            BUCKETID = "BUCKETID2",
                            PalletNo = "PalletNo",
                            ATSPalletNo = "ATSPalletNo",
                            SN = new object[]{
                                new { SN = "SN31"}
                            }
                        }
                    }
                };
                var s = getWS().PPSBOMReleaseResponse(JsonConvert.SerializeObject(model));
                txtResult.Text = s;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnATSPickComplete_Click(object sender, EventArgs e)
        {
            try
            {
                var model = new
                {
                    TaskNo = Guid.NewGuid().ToString(),
                    OPTYPE = "ATSPick",
                    PALLETNO = "PALLETNO",
                    AUART = "AUART",
                    CARTONS = new object[] {
                        new {
                            ICT_PARTNO = "ICT_PARTNO1",
                            CARTONID = "CARTONID1",
                            SN = new object[]{
                                new { SN = "SN11"},
                                new { SN = "SN12"},
                                new { SN = "SN13"},
                                new { SN = "SN14"}
                            }
                        },
                        new {
                            ICT_PARTNO = "ICT_PARTNO2",
                            CARTONID = "CARTONID2",
                            SN = new object[]{
                                new { SN = "SN21"}
                            }
                        },
                        new {
                            ICT_PARTNO = "ICT_PARTNO2",
                            CARTONID = "CARTONID3",
                            SN = new object[]{
                                new { SN = "SN31"}
                            }
                        }
                    }
                };
                var s = getWS().ATSPickComplete(JsonConvert.SerializeObject(model));
                txtResult.Text = s;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnStockInConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                var model = new
                {
                    TaskNo = Guid.NewGuid().ToString(),
                    OPTYPE = "StockInConfirm",
                    CARS = new object[] {
                        new {
                            WAREHOUSE_NO = "WAREHOUSE_NO",
                            LOCATION_NO = "LOCATION_NO",
                            TROLLEYID = "TROLLEYID",
                            CARTONS = new object[]{
                                new {
                                    ICT_PARTNO = "ICT_PARTNO",
                                    CARTONID = "CARTONID",
                                    TROLLEY = new {ID="ID",LINENO="LINENO" },
                                    SN = new object[]{
                                        new { SN = "SN1"},
                                        new { SN = "SN2"},
                                        new { SN = "SN3"},
                                        new { SN = "SN4"}
                                    }
                                }
                            }
                        }
                    }
                };
                var s = getWS().StockInConfirm(JsonConvert.SerializeObject(model));
                txtResult.Text = s;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTrolleyMoveNotice_Click(object sender, EventArgs e)
        {
            try
            {
                var model = new
                {
                    TaskNo = Guid.NewGuid().ToString(),
                    OPTYPE = "PickComplete",
                    CARS = new object[] {
                        new {
                            WAREHOUSE_NO = "WAREHOUSE_NO",
                            LOCATION_NO = "LOCATION_NO",
                            TROLLEYID = "TROLLEYID",
                            CARTONS = new object[]{
                                new {
                                    ICT_PARTNO = "ICT_PARTNO",
                                    CARTONID = "CARTONID",
                                    TROLLEY = new {ID="ID",LINENO="LINENO" },
                                    SN = new object[]{
                                        new { SN = "SN1"},
                                        new { SN = "SN2"},
                                        new { SN = "SN3"},
                                        new { SN = "SN4"}
                                    }
                                }
                            }
                        }
                    }
                };
                var s = getWS().TrolleyMoveNotice(JsonConvert.SerializeObject(model));
                txtResult.Text = s;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //http://localhost:8101/ICTGetechWCF
            string strurl = "http://localhost:8101/PPSSelfJobWcf";
            try
            {
                
                var s = getWS(strurl).AutoWMSMarinaCheck(2, 10);
                txtResult.Text = s;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
