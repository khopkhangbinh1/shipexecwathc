package com.ict.ppsedi.model;

import com.google.gson.Gson;
import com.ict.ppsedi.entities.ExecuteResult;
import com.ict.ppsedi.entities.PalletInfoEntity;
import com.ict.ppsedi.entities.PickSNEntity;
import com.ict.ppsedi.entities.T_SHIPMENT_PALLET_EXT;
import com.ict.ppsedi.utilities.UtilsConstants;

import org.json.JSONObject;

import java.util.ArrayList;
import java.util.Date;
import java.util.concurrent.TimeUnit;

import okhttp3.FormBody;
import okhttp3.HttpUrl;
import okhttp3.MediaType;
import okhttp3.MultipartBody;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.RequestBody;
import okhttp3.Response;

public class PickModel {
    private Gson gson;
    private OkHttpClient client;

    public PickModel() {
        gson = new Gson();
//        client = new OkHttpClient();
        client = new OkHttpClient.Builder()
                .connectTimeout(30, TimeUnit.MINUTES)
                .writeTimeout(30, TimeUnit.SECONDS)
                .readTimeout(30, TimeUnit.SECONDS)
                .build();
    }

    public ExecuteResult GetListPallet(String shipDate, String shipmentid) {
        ExecuteResult result = new ExecuteResult();
        String path = UtilsConstants.WEB_API_ROOT + "/api/ppspick/GetPickTaskSummaryList";
        HttpUrl.Builder urlBuilder = HttpUrl.parse(path).newBuilder();
        urlBuilder.addQueryParameter("shipDate", shipDate);
        urlBuilder.addQueryParameter("shipmentid", shipmentid);
        String url = urlBuilder.build().toString();

        Request request = new Request.Builder()
                .url(url)
                .build();
        try {
            Response response = client.newCall(request).execute();
            if (response.isSuccessful()) {
                result = gson.fromJson(response.body().string(), ExecuteResult.class);
            } else {
                result.setMessage(response.message());
            }
        } catch (Exception ex) {
            result.setMessage(ex.getMessage());
        }
        return result;
    }

    public ExecuteResult getValidPalletPick(String palletID, String machineCode) {
        ExecuteResult result = new ExecuteResult();
        try {
            MediaType JSON
                    = MediaType.parse("application/json; charset=utf-8");
            String url = UtilsConstants.WEB_API_ROOT + "/api/ppspick/BindPickMechine";

            JSONObject json = new JSONObject();
            json.put("PalletId", palletID);
            json.put("uuid", machineCode);

            RequestBody body = RequestBody.create(JSON, json.toString());
            Request request = new Request.Builder()
                    .url(url)
                    .post(body)
                    .build();
            Response response = client.newCall(request).execute();
            if (response.isSuccessful()) {
                result = gson.fromJson(response.body().string(), ExecuteResult.class);
            } else {
                result.setMessage(response.message());
            }
        } catch (Exception ex) {
            result.setMessage(ex.getMessage());
        }
        return result;
    }

    public ExecuteResult getPalletInfoDetail(String palletId) {
        ExecuteResult result = new ExecuteResult();
        String path = UtilsConstants.WEB_API_ROOT + "/api/ppspick/GetPickPalletInfo";
        HttpUrl.Builder urlBuilder = HttpUrl.parse(path).newBuilder();
        urlBuilder.addQueryParameter("palletId", palletId);
        String url = urlBuilder.build().toString();

        Request request = new Request.Builder()
                .url(url)
                .build();
        try {
            Response response = client.newCall(request).execute();
            if (response.isSuccessful()) {
                result = gson.fromJson(response.body().string(), ExecuteResult.class);
            } else {
                result.setMessage(response.message());
            }
        } catch (Exception ex) {
            result.setMessage(ex.getMessage());
        }
        return result;
    }

    public ExecuteResult getPalletPart(String palletId) {
        ExecuteResult result = new ExecuteResult();
        String path = UtilsConstants.WEB_API_ROOT + "/api/ppspick/GetPickTaskItem";
        HttpUrl.Builder urlBuilder = HttpUrl.parse(path).newBuilder();
        urlBuilder.addQueryParameter("palletId", palletId);
        urlBuilder.addQueryParameter("area", "");
        String url = urlBuilder.build().toString();

        Request request = new Request.Builder()
                .url(url)
                .build();
        try {
            Response response = client.newCall(request).execute();
            if (response.isSuccessful()) {
                result = gson.fromJson(response.body().string(), ExecuteResult.class);
            } else {
                result.setMessage(response.message());
            }
        } catch (Exception ex) {
            result.setMessage(ex.getMessage());
        }
        return result;
    }

    public ExecuteResult getLocationDetail(String palletId, String partNo) {
        ExecuteResult result = new ExecuteResult();
        String path = UtilsConstants.WEB_API_ROOT + "/api/ppspick/GetTaskStockLoc";
        HttpUrl.Builder urlBuilder = HttpUrl.parse(path).newBuilder();
        urlBuilder.addQueryParameter("palletId", palletId);
        urlBuilder.addQueryParameter("partNo", partNo);
        String url = urlBuilder.build().toString();

        Request request = new Request.Builder()
                .url(url)
                .build();
        try {
            Response response = client.newCall(request).execute();
            if (response.isSuccessful()) {
                result = gson.fromJson(response.body().string(), ExecuteResult.class);
            } else {
                result.setMessage(response.message());
            }
        } catch (Exception ex) {
            result.setMessage(ex.getMessage());
        }
        return result;
    }

    public ExecuteResult pickCTN(PickSNEntity objSN) {
        ExecuteResult result = new ExecuteResult();
        try {
            MediaType JSON
                    = MediaType.parse("application/json; charset=utf-8");
            String url = UtilsConstants.WEB_API_ROOT + "/api/ppspick/PickCTN";
            String json = gson.toJson(objSN);

            RequestBody body = RequestBody.create(JSON, json);
            Request request = new Request.Builder()
                    .url(url)
                    .post(body)
                    .build();
            Response response = client.newCall(request).execute();
            if (response.isSuccessful()) {
                result = gson.fromJson(response.body().string(), ExecuteResult.class);
            } else {
                result.setMessage(response.message());
            }
        } catch (Exception ex) {
            result.setMessage(ex.getMessage());
        }
        return result;
    }

    public ExecuteResult finishPick(String palletNO) {
        ExecuteResult result = new ExecuteResult();
        try {
            String url = UtilsConstants.WEB_API_ROOT + "/api/ppspick/pickend?strpalletno=" + palletNO;
            RequestBody formBody = new FormBody.Builder()
                    .add("strpalletno", palletNO)
                    .build();

            RequestBody body = RequestBody.create(null, new byte[]{});

            Request request = new Request.Builder()
                    .url(url)
                    .post(body)
                    .build();

            Response response = client.newCall(request).execute();
            if (response.isSuccessful()) {
                result = gson.fromJson(response.body().string(), ExecuteResult.class);
            } else {
                result.setMessage(response.message());
            }
        } catch (Exception ex) {
            result.setMessage(ex.getMessage());
        }
        return result;
    }

    public ExecuteResult getPickLog(String uuid, String pickdate) {
        ExecuteResult result = new ExecuteResult();
        try {
            String path = UtilsConstants.WEB_API_ROOT + "/api/ppspick/getpicklogbydate";
            HttpUrl.Builder urlBuilder = HttpUrl.parse(path).newBuilder();
            urlBuilder.addQueryParameter("uuid", uuid);
            urlBuilder.addQueryParameter("pickdate", pickdate);
            String url = urlBuilder.build().toString();

            Request request = new Request.Builder()
                    .url(url)
                    .build();

            Response response = client.newCall(request).execute();
            if (response.isSuccessful()) {
                result = gson.fromJson(response.body().string(), ExecuteResult.class);
            } else {
                result.setMessage(response.message());
            }
        } catch (Exception ex) {
            result.setMessage(ex.getMessage());
        }
        return result;
    }

    public ExecuteResult getPickPalletLabelInfo(String cartonNo) {
        ExecuteResult result = new ExecuteResult();
        try {
            String path = UtilsConstants.WEB_API_ROOT + "/api/ppspick/PrintPickLabel";
            HttpUrl.Builder urlBuilder = HttpUrl.parse(path).newBuilder();
            urlBuilder.addQueryParameter("cartonno", cartonNo);
            String url = urlBuilder.build().toString();

            Request request = new Request.Builder()
                    .url(url)
                    .build();

            Response response = client.newCall(request).execute();
            if (response.isSuccessful()) {
                result = gson.fromJson(response.body().string(), ExecuteResult.class);
            } else {
                result.setMessage(response.message());
            }
        } catch (Exception ex) {
            result.setMessage(ex.getMessage());
        }
        return result;
    }
}
