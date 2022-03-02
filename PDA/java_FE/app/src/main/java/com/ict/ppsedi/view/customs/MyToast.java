package com.ict.ppsedi.view.customs;

import android.content.Context;
import android.graphics.Color;
import android.graphics.PorterDuff;
import android.os.CountDownTimer;
import android.view.Gravity;
import android.view.View;
import android.widget.TextView;
import android.widget.Toast;

import com.ict.ppsedi.bluetoothPrinterHelper.App;
import com.ict.ppsedi.utilities.UtilsConstants;

public class MyToast {
//    private static Toast mToastToShow;
    private static Toast toast;

    public static void show(Context c, String msg, String type) {
        if (toast == null) {
            toast = Toast.makeText(c, msg, Toast.LENGTH_SHORT);
            toast.setGravity(Gravity.TOP, 0, 0);
        } else {
            toast.cancel();
            toast = Toast.makeText(c, msg, Toast.LENGTH_SHORT);
            toast.setGravity(Gravity.TOP, 0, 0);
        }
        View view = toast.getView();
        TextView v = (TextView) toast.getView().findViewById(android.R.id.message);
        v.setTextColor(Color.WHITE);
        if (type.equals(UtilsConstants.Status.SUCCESS.name()))
            view.getBackground().setColorFilter(Color.parseColor("#5fba7d"), PorterDuff.Mode.SRC_IN);
        else if (type.equals(UtilsConstants.Status.WARNING.name()))
            view.getBackground().setColorFilter(Color.parseColor("#f3eb05"), PorterDuff.Mode.SRC_IN);
        else if (type.equals(UtilsConstants.Status.ERROR.name())) {
//            makeLongToast(c, msg, view, 15000);
//            toast.setDuration(Toast.LENGTH_LONG);
            view.getBackground().setColorFilter(Color.parseColor("#ef1d1d"), PorterDuff.Mode.SRC_IN);
            makeLongToast(c, view, msg);
            return;
        }
        toast.show();
    }

    public static void makeLongToast(Context c, View view, String msg) {
        int toastDurationInMilliSeconds = 5000;
        if (toast == null) {
            toast = Toast.makeText(c, msg, Toast.LENGTH_LONG);
            toast.setGravity(Gravity.TOP, 0, 0);
            toast.setView(view);
        } else {
            toast.cancel();
            toast = Toast.makeText(c, msg, Toast.LENGTH_LONG);
            toast.setGravity(Gravity.TOP, 0, 0);
            toast.setView(view);
        }
        CountDownTimer toastCountDown;
        toastCountDown = new CountDownTimer(toastDurationInMilliSeconds, 1000 /*Tick duration*/) {
            public void onTick(long millisUntilFinished) {
                toast.show();
            }

            public void onFinish() {
                toast.cancel();
            }
        };
        toast.show();
        toastCountDown.start();
    }

    public static void show(Context c, String msg, String type, int typeToast) {
        Toast toast = Toast.makeText(c, msg, typeToast);
        toast.setGravity(Gravity.CENTER, 0, 0);
        View view = toast.getView();
        TextView v = (TextView) toast.getView().findViewById(android.R.id.message);
        v.setTextColor(Color.WHITE);
        if (type.equals(UtilsConstants.Status.SUCCESS.name()))
            view.getBackground().setColorFilter(Color.parseColor("#5fba7d"), PorterDuff.Mode.SRC_IN);
        else if (type.equals(UtilsConstants.Status.WARNING.name()))
            view.getBackground().setColorFilter(Color.parseColor("#e86311"), PorterDuff.Mode.SRC_IN);
        else if (type.equals(type.equals(UtilsConstants.Status.ERROR.name())))
            view.getBackground().setColorFilter(Color.parseColor("#ef1d1d"), PorterDuff.Mode.SRC_IN);
        toast.show();
    }
}