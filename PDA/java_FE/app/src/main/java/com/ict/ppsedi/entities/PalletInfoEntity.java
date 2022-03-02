package com.ict.ppsedi.entities;

public class PalletInfoEntity {
    String ShipmentID;
    String PalletNo;
    String PickPalletNo;
    String carrierName;
    String Region;
    String ShipmentType;
    String ShipType;
    String pickStatus;
    String PalletType;

    String Priority;
    int Qty;
    int DealQty;
    String Packcodedesc;
    String Remark;
    String CartonQtyInfo;
    String PalletNumber;

    public String getPalletNumber() {
        return PalletNumber;
    }

    public void setPalletNumber(String palletNumber) {
        PalletNumber = palletNumber;
    }

    public String getCartonQtyInfo() {
        return CartonQtyInfo;
    }

    public void setCartonQtyInfo(String cartonQtyInfo) {
        CartonQtyInfo = cartonQtyInfo;
    }

    public String getPickPalletNo() {
        return PickPalletNo;
    }

    public void setPickPalletNo(String pickPalletNo) {
        PickPalletNo = pickPalletNo;
    }

    public String getRemark() {
        return Remark;
    }

    public void setRemark(String remark) {
        Remark = remark;
    }

    public String getPalletType() {
        return PalletType;
    }

    public void setPalletType(String palletType) {
        PalletType = palletType;
    }

    public String getShipmentID() {
        return ShipmentID;
    }

    public void setShipmentID(String shipmentID) {
        ShipmentID = shipmentID;
    }

    public String getPalletNo() {
        return PalletNo;
    }

    public void setPalletNo(String palletNo) {
        PalletNo = palletNo;
    }

    public String getCarrierName() {
        return carrierName;
    }

    public void setCarrierName(String carrierName) {
        this.carrierName = carrierName;
    }

    public String getRegion() {
        return Region;
    }

    public void setRegion(String region) {
        Region = region;
    }

    public String getShipmentType() {
        return ShipmentType;
    }

    public void setShipmentType(String shipmentType) {
        ShipmentType = shipmentType;
    }

    public String getShipType() {
        return ShipType;
    }

    public void setShipType(String shipType) {
        ShipType = shipType;
    }

    public String getPickStatus() {
        return pickStatus;
    }

    public void setPickStatus(String pickStatus) {
        this.pickStatus = pickStatus;
    }

    public String getPriority() {
        return Priority;
    }

    public void setPriority(String priority) {
        Priority = priority;
    }

    public int getQty() {
        return Qty;
    }

    public void setQty(int qty) {
        Qty = qty;
    }

    public int getDealQty() {
        return DealQty;
    }

    public void setDealQty(int dealQty) {
        DealQty = dealQty;
    }

    public String getPackcodedesc() {
        return Packcodedesc;
    }

    public void setPackcodedesc(String packcodedesc) {
        Packcodedesc = packcodedesc;
    }
}
