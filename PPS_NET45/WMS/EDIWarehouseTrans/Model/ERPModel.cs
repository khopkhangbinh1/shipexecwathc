using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDIWareHouseTrans
{

    /// <summary>
    /// 工单信息
    /// </summary>
    public class ERPWOInModel
    {
        /// <summary>
        /// 数据记录号
        /// </summary>
        public string ZRECORD_NO { get; set; }
        /// <summary>
        /// 工厂
        /// </summary>
        public string WERKS { get; set; }
        /// <summary>
        /// 表头物料
        /// </summary>
        public string MATNR_H { get; set; }
        /// <summary>
        /// 表带物料
        /// </summary>
        public string MATNR_B { get; set; }
        /// <summary>
        /// Gift标识
        /// </summary>
        public string ZGIFT_X { get; set; }
        /// <summary>
        /// 工单号
        /// </summary>
        public string AUFNR { get; set; }
        /// <summary>
        /// 工单料号
        /// </summary>
        public string PLNBEZ { get; set; }
        /// <summary>
        /// 工单数量
        /// </summary>
        public int GAMNG { get; set; }

    }

    public class ERPWOBatchInModel
    {
        /// <summary>
        /// 批次號
        /// </summary>
        public string PACK { get; set; }
        public string PACK_CONT
        {
            get
            {
                return items == null ? null : items.Count.ToString();
            }
        }
        /// <summary>
        /// 項次
        /// </summary>
        public List<ERPWOInModel> items { get; set; }

    }

    /// <summary>
    /// 价格
    /// </summary>
    //public class ERPPriceInModel
    //{
    //    public List<ERPPriceModel> items { get; set; }
    //}

    /// <summary>
    /// 客户物料
    /// </summary>
    public class ERPICTPNInModel
    {
        /// <summary>
        /// 销售组织
        /// </summary>
        public string VKORG { get; set; }
        /// <summary>
        /// 物料
        /// </summary>
        public string MATNR { get; set; }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public DateTime LAEDA { get; set; }
    }
    /// <summary>
    /// 生产工厂出货 Header
    /// </summary>
    public class ERPFactoryShipInModel
    {
        /// <summary>
        /// 销售凭证类型
        /// </summary>
        public string AUART { get; set; }
        /// <summary>
        /// 销售组织
        /// </summary>
        public string VKORG { get; set; }
        /// <summary>
        /// 分销渠道
        /// </summary>
        public string VTWEG { get; set; }
        /// <summary>
        /// 产品组
        /// </summary>
        public string SPART { get; set; }
        /// <summary>
        /// 售达方
        /// </summary>
        public string KUNAG { get; set; }
        /// <summary>
        /// 送达方
        /// </summary>
        public string KUNWE { get; set; }
        /// <summary>
        /// 单据日期
        /// </summary>
        public string AUDAT
        {
            get
            {
                return CREATEDATE == null ? null : CREATEDATE.Value.ToString("yyyyMMdd");
            }
        }

        /// <summary>
        /// 单据日期
        /// </summary>
        public DateTime? CREATEDATE { get; set; }

        /// <summary>
        /// 客户参考
        /// </summary>
        public string BSTNK { get; set; }
        /// <summary>
        /// EDI810发票编号
        /// </summary>
        public string XBLNR { get; set; }
        /// <summary>
        /// 多角贸易代码
        /// </summary>
        public string KNUMH { get; set; }

        public List<ERPFactoryShipInItemModel> items { get; set; }
    }

    public class ERPFactoryShipInItemModel
    {

        /// <summary>
        /// 物料
        /// </summary>
        public string MATNR { get; set; }
        /// <summary>
        /// 订单数量
        /// </summary>
        public string KWMENG
        {
            get
            {
                return QTY == null ? null : QTY.ToString();
            }
        }

        /// <summary>
        /// 订单数量
        /// </summary>
        public decimal? QTY { get; set; }

        /// <summary>
        /// 销售单位
        /// </summary>
        public string VRKME { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public string NETPR
        {
            get
            {
                return PRICE == null ? null : PRICE.ToString();
            }
        }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal? PRICE { get; set; }

        /// <summary>
        /// 货币
        /// </summary>
        public string WAERS { get; set; }
        /// <summary>
        /// 税码
        /// </summary>
        public string MWSKZ { get; set; }
        /// <summary>
        /// 工厂
        /// </summary>
        public string WERKS { get; set; }
        /// <summary>
        /// 库存地点
        /// </summary>
        public string LGORT { get; set; }
        /// <summary>
        /// 发货库存地点
        /// </summary>
        public string RESLO { get; set; }
        /// <summary>
        /// 批号
        /// </summary>
        public string CHARG { get; set; }
    }

    /// <summary>
    /// 接单公司出货
    /// </summary>
    public class ERPCompanyShipInModel
    {
        /// <summary>
        /// 销售凭证类型
        /// </summary>
        public string AUART { get; set; }
        /// <summary>
        /// 销售组织
        /// </summary>
        public string VKORG { get; set; }
        /// <summary>
        /// 分销渠道
        /// </summary>
        public string VTWEG { get; set; }
        /// <summary>
        /// 产品组
        /// </summary>
        public string SPART { get; set; }
        /// <summary>
        /// 售达方
        /// </summary>
        public string KUNAG { get; set; }
        /// <summary>
        /// 送达方
        /// </summary>
        public string KUNWE { get; set; }
        /// <summary>
        /// 单据日期
        /// </summary>
        public string AUDAT
        {
            get
            {
                return CREATEDATE == null ? null : CREATEDATE.Value.ToString("yyyyMMdd");
            }
        }

        /// <summary>
        /// 单据日期
        /// </summary>
        public DateTime? CREATEDATE { get; set; }
        /// <summary>
        /// 客户参考
        /// </summary>
        public string BSTNK { get; set; }
        /// <summary>
        /// EDI810发票编号
        /// </summary>
        public string XBLNR { get; set; }
        /// <summary>
        /// 多角贸易代码
        /// </summary>
        public string KNUMH { get; set; }

        public List<ERPCompanyShipInItemModel> items { get; set; }

    }

    /// <summary>
    /// 接单公司出货 Item
    /// </summary>
    public class ERPCompanyShipInItemModel
    {
        /// <summary>
        /// 物料
        /// </summary>
        public string MATNR { get; set; }
        /// <summary>
        /// 订单数量
        /// </summary>
        public string KWMENG
        {
            get
            {
                return QTY == null ? null : QTY.ToString();
            }
        }

        /// <summary>
        /// 订单数量
        /// </summary>
        public decimal? QTY { get; set; }

        /// <summary>
        /// 销售单位
        /// </summary>
        public string VRKME { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public string NETPR
        {
            get
            {
                return PRICE == null ? null : PRICE.ToString();
            }
        }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal? PRICE { get; set; }

        /// <summary>
        /// 货币
        /// </summary>
        public string WAERS { get; set; }
        /// <summary>
        /// 税码
        /// </summary>
        public string MWSKZ { get; set; }
        /// <summary>
        /// 工厂
        /// </summary>
        public string WERKS { get; set; }
        /// <summary>
        /// 库存地点
        /// </summary>
        public string LGORT { get; set; }
        /// <summary>
        /// 发货库存地点
        /// </summary>
        public string RESLO { get; set; }
        /// <summary>
        /// 批号
        /// </summary>
        public string CHARG { get; set; }
    }


    /// <summary>
    /// 生产工厂和接单公司一步出货DS
    /// </summary>
    public class ERPFactoryCompanyShipInModel
    {
        /// <summary>
        /// 销售凭证类型
        /// </summary>
        public string AUART { get; set; }
        /// <summary>
        /// 销售组织
        /// </summary>
        public string VKORG { get; set; }
        /// <summary>
        /// 分销渠道
        /// </summary>
        public string VTWEG { get; set; }
        /// <summary>
        /// 产品组
        /// </summary>
        public string SPART { get; set; }
        /// <summary>
        /// 售达方
        /// </summary>
        public string KUNAG { get; set; }
        /// <summary>
        /// 送达方
        /// </summary>
        public string KUNWE { get; set; }
        /// <summary>
        /// 单据日期
        /// </summary>
        public string AUDAT
        {
            get
            {
                return CREATEDATE == null ? null : CREATEDATE.Value.ToString("yyyyMMdd");
            }
        }

        /// <summary>
        /// 单据日期
        /// </summary>
        public DateTime? CREATEDATE { get; set; }
        /// <summary>
        /// 客户参考
        /// </summary>
        public string BSTNK { get; set; }
        /// <summary>
        /// EDI810发票编号
        /// </summary>
        public string XBLNR { get; set; }
        /// <summary>
        /// 多角贸易代码
        /// </summary>
        public string KNUMH { get; set; }

        public List<ERPFactoryCompanyShipInItemModel> items { get; set; }


    }

    /// <summary>
    /// 生产工厂和接单公司一步出货DS Item
    /// </summary>
    public class ERPFactoryCompanyShipInItemModel
    {
        /// <summary>
        /// 物料
        /// </summary>
        public string MATNR { get; set; }
        /// <summary>
        /// 订单数量
        /// </summary>
        public string KWMENG
        {
            get
            {
                return QTY == null ? null : QTY.ToString();
            }
        }

        /// <summary>
        /// 订单数量
        /// </summary>
        public decimal? QTY { get; set; }

        /// <summary>
        /// 销售单位
        /// </summary>
        public string VRKME { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public string NETPR
        {
            get
            {
                return PRICE == null ? null : PRICE.ToString();
            }
        }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal? PRICE { get; set; }


        /// <summary>
        /// 货币
        /// </summary>
        public string WAERS { get; set; }
        /// <summary>
        /// 税码
        /// </summary>
        public string MWSKZ { get; set; }
        /// <summary>
        /// 工厂
        /// </summary>
        public string WERKS { get; set; }
        /// <summary>
        /// 库存地点
        /// </summary>
        public string LGORT { get; set; }
        /// <summary>
        /// 发货库存地点
        /// </summary>
        public string RESLO { get; set; }
        /// <summary>
        /// 批号
        /// </summary>
        public string CHARG { get; set; }

        /// <summary>
        /// 专案代码
        /// </summary>
        public string AUFNR { get; set; }
    }

    /// <summary>
    /// EDI810发票编号
    /// </summary>
    public class ERP810InvoiceInModel
    {
        /// <summary>
        /// 销售订单编号
        /// </summary>
        public string VBELN { get; set; }
        /// <summary>
        /// EDI810发票编号
        /// </summary>
        public string XBLNR { get; set; }
    }


    /// <summary>
    /// PPS 入库 Model
    /// </summary>
    public class ERPStockInModel
    {
        /// <summary>
        /// 栈板号
        /// </summary>
        public string SPCQN { get; set; }

        /// <summary>
        /// 生产日期  入库日  
        /// </summary>
        public string HSDAT { get; set; }

        public List<ERPStockInItemModel> items { get; set; }
    }

    public class ERPStockInItemModel
    {

        /// <summary>
        /// 检验日期  非必填
        /// </summary>
        public string QMDAT { get; set; }

        /// <summary>
        /// 检验时间 非必填
        /// </summary>
        public string QMTIM { get; set; }

        /// <summary>
        /// 工单号
        /// </summary>
        public string AUFNR { get; set; }

        /// <summary>
        /// 物料编号  
        /// </summary>
        public string MATNR { get; set; }

        /// <summary>
        /// 基本计量单位  
        /// </summary>
        public string MEINS { get; set; }

        /// <summary>
        /// 入库量
        /// </summary>
        public string GAMNG { get; set; }

        /// <summary>
        /// 用户名    
        /// </summary>
        public string UNAME { get; set; }

        /// <summary>
        /// 库存地点 非必填
        /// </summary>
        public string LGORT { get; set; }

        /// <summary>
        /// 送检日期 非必填    
        /// </summary>
        public string ZSJRQ { get; set; }

        /// <summary>
        /// 备注  非必填   
        /// </summary>
        public string REMARK { get; set; }


        /// <summary>
        /// 批次 工单号    
        /// </summary>
        public string CHARG { get; set; }

        /// <summary>
        /// 单/多包  非必填  
        /// </summary>
        public string ZSSMB { get; set; }

    }

    /// <summary>
    /// 雜收發 Model
    /// </summary>
    #region 雜收發 Model
    public class ERPZSFModel
    {
        /// <summary>
        /// 部门领料单
        /// </summary>
        public string LDDNUM { get; set; }
        /// <summary>
        /// 线别
        /// </summary>
        public string ZXIANB { get; set; }
        /// <summary>
        /// 公司代码
        /// </summary>
        public string BUKRS { get; set; }
        /// <summary>
        /// 工厂
        /// </summary>
        public string WERKS { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string NAME1 { get; set; }
        /// <summary>
        /// 领料部门
        /// </summary>
        public string WEMPF { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string BUMEN { get; set; }
        /// <summary>
        /// 移动原因码
        /// </summary>
        public string GRUND { get; set; }
        /// <summary>
        /// 移动原因描述
        /// </summary>
        public string GRTXT { get; set; }
        /// <summary>
        /// 总账科目编号
        /// </summary>
        public string SAKNR { get; set; }

        /// <summary>
        /// 成本中心
        /// </summary>
        public string KOSTL { get; set; }
        /// <summary>
        /// 移动类型(库存管理)
        /// </summary>
        public string BWART { get; set; }
        /// <summary>
        /// 移动类型描述
        /// </summary>
        public string BTEXT { get; set; }

        /// <summary>
        /// 主资产号
        /// </summary>
        public string ANLN1 { get; set; }
        /// <summary>
        /// 工作分解结构元素 (WBS 元素)
        /// </summary>
        public string POSID { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string AUFNR { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string USNAM { get; set; }
        public string ERDAT { get; set; }
        public string UZEIT { get; set; }
        public DateTime? ERDATT
        {
            get
            {
                System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;
                return string.IsNullOrEmpty(ERDAT) || string.IsNullOrEmpty(UZEIT) ?
                    null : ((DateTime?)DateTime.ParseExact(ERDAT + " " + UZEIT, "yyyyMMdd HHmmss", provider));
            }
        }

        public string DEL_FLAG { get; set; }
        public string ZBZ { get; set; }

        public DateTime CDT
        {
            get
            {
                return DateTime.Now;
            }
        }

        public List<ERPZSFItemModel> items { get; set; }
    }

    public class ERPZSFItemModel
    {
        /// <summary>
        /// 部门领料单
        /// </summary>
        public string LDDNUM { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public string SORTID { get; set; }
        /// <summary>
        ///  物料
        /// </summary>
        public string MATNR { get; set; }
        /// <summary>
        /// 物料描述
        /// </summary>
        public string MAKTX { get; set; }
        /// <summary>
        /// MRP控制者
        /// </summary>
        public string DISPO { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public string BDMNG { get; set; }
        public decimal QTY
        {
            get
            {
                return string.IsNullOrEmpty(BDMNG) ? 0 : decimal.Parse(BDMNG);
            }

        }
        public decimal DEAL_QTY { get; set; }

        /// <summary>
        /// 基本计量单位
        /// </summary>
        public string MEINS { get; set; }

        /// <summary>
        /// 库存地点
        /// </summary>
        public string LGORT { get; set; }
        /// <summary>
        /// 需求日期
        /// </summary>
        public string BDTER { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string REMARK { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string CHARG { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public string ZZSTAGE { get; set; }

        /// <summary>
        ///  删除标记
        /// </summary>
        public string DEL_FLAG { get; set; }

        /// <summary>
        /// CDT
        /// </summary>
        public DateTime CDT
        {
            get
            {
                return DateTime.Now;
            }
        }

        /// <summary>
        /// CDT
        /// </summary>
        public DateTime UDT
        {
            get
            {
                return DateTime.Now;
            }
        }
    }

    public class ERPZSFToERPModel
    {

        /// <summary>
        /// 外围系统唯一编号
        /// </summary>
        public string ZID { get; set; }
        /// <summary>
        /// 部门领料单
        /// </summary>
        public string LDDNUM { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public string SORTID { get; set; }
        /// <summary>
        /// 物料
        /// </summary>
        public string MATNR { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public string PDMNG { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string CHARG { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public string ZZSTAGE { get; set; }

        /// <summary>
        /// 过账日期
        /// </summary>
        public string BUDAT { get; set; }

        /// <summary>
        /// 过账日期
        /// </summary>
        public string ZSYFLG { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public string ZUSERS { get; set; }

    }
    #endregion

    /// <summary>
    /// 转仓 Model ZTL
    /// </summary>
    /// 
    #region 转仓 Model ZTL
    public class ERPZTLModel
    {

        /// <summary>
        /// 库存调拨单
        /// </summary>
        public string ZDBNUM { get; set; }

        /// <summary>
        /// 工厂
        /// </summary>
        public string WERKS { get; set; }

        /// <summary>
        /// 名称 1
        /// </summary>
        public string NAME1 { get; set; }

        /// <summary>
        /// 公司代码
        /// </summary>
        public string BUKRS { get; set; }
        /// <summary>
        /// 移动类型
        /// </summary>
        public string BWART { get; set; }
        /// <summary>
        /// 移动类型描述
        /// </summary>
        public string BTEXT { get; set; }

        /// <summary>
        /// 需求日期
        /// </summary>
        public string RQDATE { get; set; }
        public DateTime? RQDATET
        {
            get
            {
                System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;
                return string.IsNullOrEmpty(RQDATE) ?
                    null : ((DateTime?)DateTime.ParseExact(RQDATE, "yyyyMMdd", provider));
            }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string USNAM { get; set; }
        /// <summary>
        /// SAP创建日期
        /// </summary>
        public string ERDAT { get; set; }
        public string UZEIT { get; set; }
        public DateTime? ERDATT
        {
            get
            {
                System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;
                return string.IsNullOrEmpty(ERDAT) || string.IsNullOrEmpty(UZEIT) ?
                    null : ((DateTime?)DateTime.ParseExact(ERDAT + " " + UZEIT, "yyyyMMdd HHmmss", provider));
            }
        }


        /// <summary>
        /// 删除标记
        /// </summary>
        public string DEL_FLAG { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string ZBZ { get; set; }

        public DateTime CDT
        {
            get
            {
                return DateTime.Now;
            }
        }

        public List<ERPZTLItemModel> items { get; set; }
    }
    public class ERPZTLItemModel
    {

        /// <summary>
        /// 库存调拨单
        /// </summary>
        public string ZDBNUM { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public string SORTID { get; set; }
        /// <summary>
        /// 物料
        /// </summary>
        public string MATNR { get; set; }
        /// <summary>
        /// 物料描述
        /// </summary>
        public string MAKTX { get; set; }
        /// <summary>
        /// MRP控制者
        /// </summary>
        public string DISPO { get; set; }
        /// <summary>
        /// 基本计量单位
        /// </summary>
        public string MEINS { get; set; }
        /// <summary>
        /// 发货库存地点
        /// </summary>
        public string LGORT_BC { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string CHARG_BC { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public string ZZSTAGE_BC { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public string MENGE { get; set; }
        public decimal QTY
        {
            get
            {
                return string.IsNullOrEmpty(MENGE) ? 0 : decimal.Parse(MENGE);
            }
        }
        public decimal DEAL_QTY { get; set; }


        /// <summary>
        /// 收货库存地点
        /// </summary>
        public string LGORT_BR { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string CHARG_BR { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public string ZZSTAGE_BR { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string REMARK { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public string DEL_FLAG { get; set; }
        public DateTime CDT
        {
            get
            {
                return DateTime.Now;
            }
        }
        public DateTime UDT
        {
            get
            {
                return DateTime.Now;
            }
        }

    }
    public class ERPZTLToERPModel
    {
        /// <summary>
        /// 外围系统唯一编号
        /// </summary>
        public string ZID { get; set; }

        /// <summary>
        /// 库存调拨单
        /// </summary>
        public string ZDBNUM { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public string SORTID { get; set; }
        /// <summary>
        /// 物料
        /// </summary>
        public string MATNR { get; set; }
        /// <summary>
        /// 发货库存地点 
        /// </summary>
        public string LGORT_BC { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string CHARG_BC { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public string ZZSTAGE_BC { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public string MENGE { get; set; }
        /// <summary>
        /// 收货库存地点
        /// </summary>
        public string LGORT_BR { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string CHARG_BR { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public string ZZSTAGE_BR { get; set; }
        /// <summary>
        /// 系统标识
        /// </summary>
        public string ZSYFLG { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        public string ZUSERS { get; set; }
    }
    #endregion


    /// <summary>
    /// 工單領退料 
    /// </summary>
    #region 工單領退料 
    public class ERPZBOMRModel
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string AUFNR { get; set; }
        /// <summary>
        /// 物料编号
        /// </summary>
        public string PLNBEZ { get; set; }
        /// <summary>
        /// 线别
        /// </summary>
        public string ZZLINE { get; set; }
        /// <summary>
        /// 基本开始日期
        /// </summary>
        public string GSTRP { get; set; }
        /// <summary>
        /// 基本结束日期
        /// </summary>
        public string GLTRP { get; set; }
        /// <summary>
        /// 工单总数量
        /// </summary>
        public string GAMNG { get; set; }
        public decimal QTY
        {
            get
            {
                return string.IsNullOrEmpty(GAMNG) ? 0 : decimal.Parse(GAMNG);
            }
        }
        /// <summary>
        /// 工单类型
        /// </summary>
        public string AUART { get; set; }

        /// <summary>
        /// 工单阶段
        /// </summary>
        public string ZZSTAGE { get; set; }
        /// <summary>
        /// 生产版本
        /// </summary>
        public string VERID { get; set; }
        /// <summary>
        /// 生产工厂
        /// </summary>
        public string DWERK { get; set; }
        /// <summary>
        /// 入库库存地点
        /// </summary>
        public string LGORT { get; set; }

        /// <summary>
        /// ZSTATS
        /// </summary>
        public string ZSTATS { get; set; }
        public DateTime CDT
        {
            get
            {
                return DateTime.Now;
            }
        }

        public List<ERPZBOMRItemModel> items { get; set; }
    }

    public class ERPZBOMRItemModel
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string AUFNR { get; set; }
        /// <summary>
        /// BOM项目号
        /// </summary>
        public string POSNR { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string CHARG { get; set; }
        /// <summary>
        /// 阶段
        /// </summary>
        public string ZZSTAGE { get; set; }
        /// <summary>
        /// 组件物料号
        /// </summary>
        public string MATNR { get; set; }
        /// <summary>
        /// 评估类
        /// </summary>
        public string BKLAS { get; set; }
        /// <summary>
        /// 组件需求数量
        /// </summary>
        public string BDMNG { get; set; }

        /// <summary>
        /// 组件需求数量
        /// </summary>
        public decimal Qty
        {
            get
            {
                return string.IsNullOrEmpty(BDMNG) ? 0 : decimal.Parse(BDMNG);
            }
        }

        /// <summary>
        /// 单位
        /// </summary>
        public string MEINS { get; set; }
        /// <summary>
        /// 库存地点
        /// </summary>
        public string LGORT { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public string XLOEK { get; set; }

        /// <summary>
        /// 虚拟标记
        /// </summary>
        public string DUMPS { get; set; }
        /// <summary>
        /// 替代组
        /// </summary>
        public string ALPGR { get; set; }
        /// <summary>
        /// 已经处理数量
        /// </summary>
        public decimal DEAL_QTY { get; set; }
        public DateTime CDT
        {
            get
            {
                return DateTime.Now;
            }
        }
        public DateTime UDT
        {
            get
            {
                return DateTime.Now;
            }
        }
    }

    public class ERPZBOMRToERPModel
    {
        /// <summary>
        /// 外围系统唯一标识
        /// </summary>
        public string ZID { get; set; }
        /// <summary>
        /// 生产订单号
        /// </summary>
        public string AUFNR { get; set; }
        /// <summary>
        /// BOM行
        /// </summary>
        public string POSNR { get; set; }
        /// <summary>
        /// 过账类型
        /// </summary>
        public string ZPTYPE { get; set; }
        /// <summary>
        /// 物料号
        /// </summary>
        public string MATNR { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public string PDMNG { get; set; }
        /// <summary>
        /// 库存地点
        /// </summary>
        public string LGORT { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string CHARG { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public string ZZSTAGE { get; set; }
        /// <summary>
        /// 系统标识
        /// </summary>
        public string ZSYFLG { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        public string ZUSERS { get; set; }

    }
    #endregion

    public class ERPProjectCodeInModel
    {
        /// <summary>
        /// 内部订单
        /// </summary>
        public string AUFNR { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public string AUART { get; set; }


        /// <summary>
        /// 创建日期(开始)
        /// </summary>
        public string DATBI { get; set; }

        /// <summary>
        /// 创建日期(截止)  
        /// </summary>
        public string DATAB { get; set; }

        /// <summary>
        /// 公司代码
        /// </summary>
        public string BUKRS { get; set; }
    }

    public class ERPProjectCodeModel
    {
        /// <summary>
        /// 内部订单
        /// </summary>
        public string AUFNR { get; set; }

        /// <summary>
        /// 内部订单描述
        /// </summary>
        public string LTEXT { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public string AUART { get; set; }
        /// <summary>
        /// 订单类型描述
        /// </summary>
        public string AUARTTXT { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public string ERDAT { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string ERNAM { get; set; }

        /// <summary>
        /// 公司代码
        /// </summary>
        public string BUKRS { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string BUTXT { get; set; }
        public DateTime? CDT
        {
            get
            {
                System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;
                return string.IsNullOrEmpty(ERDAT) ? null : (DateTime?)DateTime.ParseExact(ERDAT, "yyyyMMdd", provider);
            }
        }
    }

    public class PPSToERPRTModel
    {
        /// <summary>
        /// 外围系统唯一编号
        /// </summary>
        public string ZID { get; set; }

        /// <summary>
        /// 库存调拨单 
        /// </summary>
        public string ZDBNUM { get; set; }

        /// <summary>
        /// 订单号 
        /// </summary>
        public string AUFNR { get; set; }


        /// <summary>
        /// 部门领料单
        /// </summary>
        public string LDDNUM { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public string SORTID { get; set; }

        /// <summary>
        /// 物料凭证
        /// </summary>
        public string MBLNR { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public string ZZMSGTYPE { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string ZZMSG { get; set; }

        /// <summary>
        /// 消息状态
        /// </summary>
        public bool IsSuccess
        {
            get
            {
                return ZZMSGTYPE == "S";
            }
        }
    }

    public class ERPItemsInOut<T>
    {
        public List<T> items { get; set; }
    }

    public class ZSFTOERPMODEL
    {
        public string ZID { get; set; }
        public string LDDNUM { get; set; }
        public string SORTID { get; set; }
        public string MATNR { get; set; }
        public string PDMNG { get; set; }
        public string CHARG { get; set; }
        public string ZZSTAGE { get; set; }
        public string BUDAT { get; set; }
        public string ZSYFLG { get; set; }
        public string ZUSERS { get; set; }
    }
    public class ZTLTOERPMODEL
    {
        public string ZID { get; set; }
        public string ZDBNUM { get; set; }
        public string SORTID { get; set; }
        public string MATNR { get; set; }
        public string LGORT_BC { get; set; }
        public string CHARG_BC { get; set; }
        public string ZZSTAGE_BC { get; set; }
        public string MENGE { get; set; }
        public string LGORT_BR { get; set; }
        public string CHARG_BR { get; set; }
        public string ZZSTAGE_BR { get; set; }
        public string ZSYFLG { get; set; }
        public string ZUSERS { get; set; }
        public string BUDAT { get; set; }

    }

    public class ZBOMRTOERPMODEL
    {
        public string ZID { get; set; }
        public string AUFNR { get; set; }
        public string POSNR { get; set; }
        public string ZPTYPE { get; set; }
        public string MATNR { get; set; }
        public string PDMNG { get; set; }
        public string LGORT { get; set; }
        public string CHARG { get; set; }
        public string ZZSTAGE { get; set; }
        public string ZSYFLG { get; set; }
        public string ZUSERS { get; set; }
        public string BUDAT { get; set; }

    }

    public class WMSOERPTOPPSMODEL
    {
        public object ZID { get; set; }
        public object ZDBNUM { get; set; }
        public object AUFNR { get; set; }
        public object LDDNUM { get; set; }
        public object SORTID { get; set; }
        public object MBLNR { get; set; }
        public string ZZMSGTYPE { get; set; }
        public object ZZMSG { get; set; }
        public string OMSMSG { get; set; }
        public bool IsSuccess { get; set; }
    }


}
