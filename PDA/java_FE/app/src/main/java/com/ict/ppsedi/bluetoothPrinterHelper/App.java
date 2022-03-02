package com.ict.ppsedi.bluetoothPrinterHelper;

import android.app.Application;
import android.content.Context;

public class App extends Application {

    public static Context mContext;

    @Override
    public void onCreate() {
        mContext = getApplicationContext();
        super.onCreate();
    }

    public static Context getContext() {
        if (mContext == null) {
        }
        return mContext;
    }
}
