package com.ict.ppsedi.entities;

public class T_SHIPMENT_PALLET_EXT {
    String SHIPMENT_ID;
    String PALLET_NO;
    String CARRIER_NAME;
    String TYPE;
    String REGION;
    String QTY;
    String CARTON_QTY;

    public T_SHIPMENT_PALLET_EXT(){
        SHIPMENT_ID = "DY2133044669";
        PALLET_NO = "20210302117759";
        TYPE = "PARCEL";
        CARRIER_NAME = "UPS";
        REGION = "EMEIA";
        CARTON_QTY = "40";
        QTY = "240";
    }

    public String getSHIPMENT_ID() {
        return SHIPMENT_ID;
    }

    public void setSHIPMENT_ID(String SHIPMENT_ID) {
        this.SHIPMENT_ID = SHIPMENT_ID;
    }

    public String getPALLET_NO() {
        return PALLET_NO;
    }

    public void setPALLET_NO(String PALLET_NO) {
        this.PALLET_NO = PALLET_NO;
    }

    public String getQTY() {
        return QTY;
    }

    public void setQTY(String QTY) {
        this.QTY = QTY;
    }

    public String getCARTON_QTY() {
        return CARTON_QTY;
    }

    public void setCARTON_QTY(String CARTON_QTY) {
        this.CARTON_QTY = CARTON_QTY;
    }

    public String getCARRIER_NAME() {
        return CARRIER_NAME;
    }

    public void setCARRIER_NAME(String CARRIER_NAME) {
        this.CARRIER_NAME = CARRIER_NAME;
    }

    public String getTYPE() {
        return TYPE;
    }

    public void setTYPE(String TYPE) {
        this.TYPE = TYPE;
    }

    public String getREGION() {
        return REGION;
    }

    public void setREGION(String REGION) {
        this.REGION = REGION;
    }
}
