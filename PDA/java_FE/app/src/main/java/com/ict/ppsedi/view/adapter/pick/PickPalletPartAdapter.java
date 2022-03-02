package com.ict.ppsedi.view.adapter.pick;

import android.content.Context;
import android.graphics.Color;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import androidx.recyclerview.widget.RecyclerView;

import com.ict.ppsedi.R;
import com.ict.ppsedi.entities.PalletInfoEntity;
import com.ict.ppsedi.entities.PalletPartEntity;
import com.ict.ppsedi.presenter.shipping.pick.PickDetailPresenter;
import com.ict.ppsedi.presenter.shipping.pick.PickSeachPresenter;

import java.util.ArrayList;

public class PickPalletPartAdapter extends RecyclerView.Adapter<PickPalletPartAdapter.MyViewHolder> {

    private Context mContext;
    private ArrayList<PalletPartEntity> listItem;
    private LayoutInflater mLayoutInflater;
    private PickDetailPresenter presenter;
    int checkPos = -1;

    public interface ItemClickListener {
        void onClick(View view, int position, boolean isLongClick);
    }


    public PickPalletPartAdapter(Context context, ArrayList<PalletPartEntity> array1, PickDetailPresenter _presenter) {
        // TODO Auto-generated constructor stub
        this.listItem = array1;
        mLayoutInflater = LayoutInflater.from(context);
        this.mContext = context;
        presenter = _presenter;
    }


    protected class MyViewHolder extends RecyclerView.ViewHolder implements PickPalletPartAdapter.ItemClickListener {

        public TextView tvICTPN, tvQty, tvPickCTNQty, tvCTNQty, tvRestCTN;
        LinearLayout llBody;
        PickSearchAdapter.ItemClickListener itemClickListener;

        public MyViewHolder(View v) {
            super(v);
            llBody = v.findViewById(R.id.llBody);
            tvICTPN = v.findViewById(R.id.tvICTPN);
            tvQty = v.findViewById(R.id.tvQty);
            tvPickCTNQty = v.findViewById(R.id.tvPickCTNQty);
            tvCTNQty = v.findViewById(R.id.tvCTNQty);
            tvRestCTN = v.findViewById(R.id.tvRestCTN);
        }

        public void setItemClickListener(PickSearchAdapter.ItemClickListener itemClickListener) {
            this.itemClickListener = itemClickListener;
        }


        @Override
        public void onClick(View view, int position, boolean isLongClick) {
            itemClickListener.onClick(view, getAdapterPosition(), false);
        }
    }

    @Override
    public PickPalletPartAdapter.MyViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View item = mLayoutInflater.inflate(R.layout.pick_palletpart_item, parent, false);
        return new PickPalletPartAdapter.MyViewHolder(item);
    }

    @Override
    public void onBindViewHolder(PickPalletPartAdapter.MyViewHolder holder, int position) {
        PalletPartEntity obj = listItem.get(position);
        holder.tvICTPN.setText("" + obj.getICTPN());
        holder.tvCTNQty.setText("" + obj.getICTPN());
        holder.tvQty.setText("" + obj.getQTY());
        holder.tvCTNQty.setText("" + obj.getCTNQty());
        holder.tvPickCTNQty.setText("" + obj.getPICKCTN());
        holder.tvRestCTN.setText("" + obj.getRESTCTN());
        if (position % 2 == 0) {
            holder.llBody.setBackgroundColor(Color.parseColor("#FFFFFF"));
        } else
            holder.llBody.setBackgroundColor(Color.parseColor("#EFEFEF"));

        holder.llBody.setOnClickListener(v -> {
            presenter.bindPartLocation(obj.getICTPN());
            checkPos = position;
            notifyDataSetChanged();
        });
        if (obj.getRESTCTN().equals("0"))
            holder.llBody.setBackgroundColor(Color.parseColor("#09b43a"));

        if (checkPos == position)
            holder.llBody.setBackgroundColor(Color.parseColor("#0095ff"));
    }

    @Override
    public int getItemCount() {
        return listItem.size();
    }

}