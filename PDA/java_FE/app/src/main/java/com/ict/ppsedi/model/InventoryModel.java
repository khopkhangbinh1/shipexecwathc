package com.ict.ppsedi.model;

import com.google.gson.Gson;
import com.ict.ppsedi.entities.ExecuteResult;
import com.ict.ppsedi.entities.InventorySNEntity;
import com.ict.ppsedi.entities.PickSNEntity;
import com.ict.ppsedi.utilities.UtilsConstants;


import okhttp3.HttpUrl;
import okhttp3.MediaType;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.RequestBody;
import okhttp3.Response;

public class InventoryModel {
    private Gson gson;
    private OkHttpClient client;

    public InventoryModel() {
        gson = new Gson();
        client = new OkHttpClient();
    }
    public ExecuteResult getWHS() {
        ExecuteResult result = new ExecuteResult();
        String path = UtilsConstants.WEB_API_ROOT + "/api/ppscheck/getWHS";
//        HttpUrl.Builder urlBuilder = HttpUrl.parse(path).newBuilder();
//        String url = urlBuilder.build().toString();

        Request request = new Request.Builder()
                .url(path)
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
    public ExecuteResult getInventory2(String id) {
        ExecuteResult result = new ExecuteResult();
        String path = UtilsConstants.WEB_API_ROOT + "/api/ppscheck/getlocationinventory";
        HttpUrl.Builder urlBuilder = HttpUrl.parse(path).newBuilder();
        urlBuilder.addQueryParameter("id", id);
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
    public ExecuteResult GetLocationCheckLog(String LocationNO) {
        ExecuteResult result = new ExecuteResult();
        String path = UtilsConstants.WEB_API_ROOT + "/api/ppscheck/GetLocationCheckLog";
        HttpUrl.Builder urlBuilder = HttpUrl.parse(path).newBuilder();
        urlBuilder.addQueryParameter("LocationNO", LocationNO);
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
    public ExecuteResult getdatalocationDetail(String whID,String LocationNO) {
        ExecuteResult result = new ExecuteResult();
        String path = UtilsConstants.WEB_API_ROOT + "/api/ppscheck/getLocationDetail";
        HttpUrl.Builder urlBuilder = HttpUrl.parse(path).newBuilder();
        urlBuilder.addQueryParameter("whID", whID);
        urlBuilder.addQueryParameter("LocationNO", LocationNO);
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
    public ExecuteResult inventoryCTN(InventorySNEntity objSN) {
        ExecuteResult result = new ExecuteResult();
        try {
            MediaType JSON
                    = MediaType.parse("application/json; charset=utf-8");
            String url = UtilsConstants.WEB_API_ROOT + "/api/ppscheck/inventoryCTN";
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
    public ExecuteResult getdatalocationDetail1() {
        ExecuteResult result = new ExecuteResult();
        String path = UtilsConstants.WEB_API_ROOT + "/api/ppscheck/inventoryCTN";
        HttpUrl.Builder urlBuilder = HttpUrl.parse(path).newBuilder();
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
    public ExecuteResult checkPallet(InventorySNEntity objSN) {
        ExecuteResult result = new ExecuteResult();
        try {
            MediaType JSON
                    = MediaType.parse("application/json; charset=utf-8");
            String url = UtilsConstants.WEB_API_ROOT + "/api/ppscheck/inventoryPallet";
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
    public ExecuteResult getLocationSnInfo(String LocationNO) {
        ExecuteResult result = new ExecuteResult();
        String path = UtilsConstants.WEB_API_ROOT + "/api/ppscheck/getLocationSnInfo";
        HttpUrl.Builder urlBuilder = HttpUrl.parse(path).newBuilder();
        urlBuilder.addQueryParameter("LocationNO", LocationNO);
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
    public ExecuteResult GetSnInfoResult(String insn) {
        ExecuteResult result = new ExecuteResult();
        String path = UtilsConstants.WEB_API_ROOT + "/api/ppscheck/GetSnInfoBySQL";
        HttpUrl.Builder urlBuilder = HttpUrl.parse(path).newBuilder();
        urlBuilder.addQueryParameter("insn", insn);
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
    public ExecuteResult inventoryCTN2(String insn,String insn2) {
        ExecuteResult result = new ExecuteResult();
        String path = UtilsConstants.WEB_API_ROOT + "/api/ppscheck/inventoryCTN2";
        HttpUrl.Builder urlBuilder = HttpUrl.parse(path).newBuilder();
        urlBuilder.addQueryParameter("insn", insn);
        urlBuilder.addQueryParameter("insn2", insn2);
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
}