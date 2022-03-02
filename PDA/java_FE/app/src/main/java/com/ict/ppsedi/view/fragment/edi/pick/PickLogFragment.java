package com.ict.ppsedi.view.fragment.edi.pick;

import android.app.DatePickerDialog;
import android.app.ProgressDialog;
import android.os.AsyncTask;
import android.os.Bundle;
import android.provider.Settings;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.RecyclerView;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import com.ict.ppsedi.R;
import com.ict.ppsedi.entities.ExecuteResult;
import com.ict.ppsedi.entities.PalletInfoEntity;
import com.ict.ppsedi.entities.PickLogEntity;
import com.ict.ppsedi.model.PickModel;
import com.ict.ppsedi.presenter.shipping.pick.PickSeachPresenter;
import com.ict.ppsedi.utilities.UtilsConstants;
import com.ict.ppsedi.view.BaseFragment;
import com.ict.ppsedi.view.adapter.pick.PickLogAdapter;
import com.ict.ppsedi.view.adapter.pick.PickPalletPartAdapter;
import com.ict.ppsedi.view.customs.MyToast;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.Locale;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;

public class PickLogFragment extends BaseFragment {

    @BindView(R.id.etPickDate)
    public EditText etPickDate;

    @BindView(R.id.lvList)
    public RecyclerView lvList;


    protected ProgressDialog progress;
    final Calendar myCalendar = Calendar.getInstance();

    PickModel model;
    String machineCode;

    @Override
    public View baseFragmentView(LayoutInflater inflater, ViewGroup parent, Bundle savedInstanceState) {
        View rootView = inflater.inflate(R.layout.pick_log_fragment, parent, false);
        ButterKnife.bind(this, rootView);
        ((AppCompatActivity) getActivity()).getSupportActionBar().setTitle("PICK Log");

        this.progress = new ProgressDialog(this.getActivity());
        progress.setTitle("Loading");
        progress.setMessage("Wait while loading...");
        progress.setCancelable(false);

        model = new PickModel();
        allListener();
        try {
            String date = new SimpleDateFormat("yyyy-MM-dd", Locale.getDefault()).format(new Date());
            etPickDate.setText(date);
        } catch (Exception ex) {
        }
        machineCode = Settings.Secure.getString(this.getContext().getContentResolver(), Settings.Secure.ANDROID_ID);
        bindLog();
        return rootView;
    }


    public void bindLog() {
        String uuid = machineCode;
        String pickDate = etPickDate.getText().toString();
        final ExecuteResult[] res = {new ExecuteResult()};
        new AsyncTask<Void, Void, String>() {
            @Override
            protected String doInBackground(Void... voids) {
                res[0] = model.getPickLog(uuid, pickDate);
                return "OK";
            }

            @Override
            protected void onPreExecute() {
                super.onPreExecute();
            }

            protected void onPostExecute(String result) {
                super.onPostExecute(result);
                if (res[0].isSuccess()) {
                    ArrayList<PickLogEntity> lst = new Gson().fromJson(res[0].getData().toString(), new TypeToken<ArrayList<PickLogEntity>>() {
                    }.getType());
                    PickLogAdapter adapter = new PickLogAdapter(getContext(), lst);
                    lvList.setAdapter(adapter);
                    MyToast.show(getContext(), "OK", UtilsConstants.Status.SUCCESS.name());
                } else {
                    MyToast.show(getContext(), res[0].getMessage(), UtilsConstants.Status.ERROR.name());
                }
            }
        }.execute();
    }

    public void allListener() {
        DatePickerDialog.OnDateSetListener date = (view, year, monthOfYear, dayOfMonth) -> {
            // TODO Auto-generated method stub
            myCalendar.set(Calendar.YEAR, year);
            myCalendar.set(Calendar.MONTH, monthOfYear);
            myCalendar.set(Calendar.DAY_OF_MONTH, dayOfMonth);
            formatDate();
        };

        etPickDate.setOnClickListener(v -> {
            // TODO Auto-generated method stub
            new DatePickerDialog(getContext(), date, myCalendar
                    .get(Calendar.YEAR), myCalendar.get(Calendar.MONTH),
                    myCalendar.get(Calendar.DAY_OF_MONTH)).show();
        });
    }

    private void formatDate() {
        String myFormat = "yyyy-MM-dd";
        SimpleDateFormat sdf = new SimpleDateFormat(myFormat, Locale.US);
        etPickDate.setText(sdf.format(myCalendar.getTime()));
    }

    @OnClick(R.id.btnSearch)
    public void btnSearch_OnClick() {
        bindLog();
    }
}
