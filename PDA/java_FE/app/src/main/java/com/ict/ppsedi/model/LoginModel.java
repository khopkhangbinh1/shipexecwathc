package com.ict.ppsedi.model;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import com.ict.ppsedi.entities.ExecuteResult;
import com.ict.ppsedi.entities.PickPartLocationEntity;
import com.ict.ppsedi.entities.UserEntity;
import com.ict.ppsedi.utilities.OkHttpHelper;
import com.ict.ppsedi.utilities.UtilsConstants;

import org.json.JSONObject;

import java.io.IOException;
import java.util.Date;
import java.util.List;
import java.util.concurrent.Callable;
import java.util.concurrent.CountDownLatch;
import java.util.concurrent.TimeUnit;

import okhttp3.Call;
import okhttp3.Callback;
import okhttp3.HttpUrl;
import okhttp3.MediaType;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.RequestBody;
import okhttp3.Response;

public class LoginModel {
    OkHttpHelper client = new OkHttpHelper();
//    OkHttpClient client = new OkHttpClient();

    public void checkLogin(UserEntity user, Callable func) {
        String path = UtilsConstants.WEB_API_ROOT + "/api/login/login";
        HttpUrl.Builder urlBuilder = HttpUrl.parse(path).newBuilder();
        urlBuilder.addQueryParameter("userid", user.getUserID());
        urlBuilder.addQueryParameter("password", user.getPassword());
        String url = urlBuilder.build().toString();

        try {
            client.post(url, "", new Callback() {
                @Override
                public void onFailure(Call call, IOException e) {
                    try {
                        func.call();
                        user.setStatus(false);
                    } catch (Exception exception) {
                        exception.printStackTrace();
                    }
                }

                @Override
                public void onResponse(Call call, Response response) {
                    try {
                        if (response.isSuccessful()) {
                            String responseStr = response.body().string();
                            if (responseStr.contains("OK")) {
                                Gson gson = new Gson();
                                ExecuteResult result = gson.fromJson(responseStr, ExecuteResult.class);
                                UserEntity obj = new Gson().fromJson(result.getData().toString(), new TypeToken<UserEntity>() {
                                }.getType());
                                user.setStatus(true);
                                user.setUserName(obj.getUserName());
                            } else
                                user.setStatus(false);
                        } else
                            user.setStatus(false);
                        func.call();
                    } catch (Exception exception) {
                        exception.printStackTrace();
                    }
                }
            });
        } catch (Exception ex) {
        }
    }

    public ExecuteResult getPDAVersion() {
        Gson gson = new Gson();
        OkHttpClient client = new OkHttpClient.Builder()
                .connectTimeout(30, TimeUnit.MINUTES)
                .writeTimeout(30, TimeUnit.SECONDS)
                .readTimeout(30, TimeUnit.SECONDS)
                .build();
        ExecuteResult result = new ExecuteResult();
        String path = UtilsConstants.WEB_API_ROOT + "/api/login/getpdaversion";
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
}
