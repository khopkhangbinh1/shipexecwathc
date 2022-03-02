package com.ict.ppsedi.view.adapter.check;

import android.content.Context;
import android.graphics.Color;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.LinearLayout;
import android.widget.TextView;

import androidx.recyclerview.widget.RecyclerView;

import com.ict.ppsedi.R;
import com.ict.ppsedi.entities.InventoryEntity;

import java.util.ArrayList;

public class InventoryAdapterSNResult extends RecyclerView.Adapter<InventoryAdapterSNResult.MyViewHolder> {
    private Context mContext;
    private ArrayList<InventoryEntity> listItem;
    private LayoutInflater mLayoutInflater;

    public interface ItemClickListener {
        void onClick(View view, int position, boolean isLongClick);
    }

    public InventoryAdapterSNResult(Context context, ArrayList<InventoryEntity> array1) {
        // TODO Auto-generated constructor stub
        this.listItem = array1;
        mLayoutInflater = LayoutInflater.from(context);
        this.mContext = context;
    }

    protected class MyViewHolder extends RecyclerView.ViewHolder implements com.ict.ppsedi.view.adapter.check.InventoryAdapterSNResult.ItemClickListener {

        public TextView tvPallet, tvPartNo, tvCTN,tvlocationNo, tvNo;
        LinearLayout llBodyInventorySNResult;
        InventoryAdapterSNResult.ItemClickListener itemClickListener;

        public MyViewHolder(View v) {
            super(v);
            llBodyInventorySNResult = v.findViewById(R.id.llBodyInventorySNResult);
            tvPallet= v.findViewById(R.id.tvPallet);
            tvCTN= v.findViewById(R.id.tvCTN);
            tvNo= v.findViewById(R.id.tvNo);
            tvPartNo = v.findViewById(R.id.tvPartNo);
           tvlocationNo = v.findViewById(R.id.tvlocationNo);
        }

        public void setItemClickListener(InventoryAdapterSNResult.ItemClickListener itemClickListener) {
            this.itemClickListener = itemClickListener;
        }

        @Override
        public void onClick(View view, int position, boolean isLongClick) {
            itemClickListener.onClick(view, getAdapterPosition(), false);
        }
    }

    @Override
    public InventoryAdapterSNResult.MyViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View item = mLayoutInflater.inflate(R.layout.inventory_sn_info_result, parent, false);
        return new InventoryAdapterSNResult.MyViewHolder(item);
    }

    @Override
    public void onBindViewHolder(InventoryAdapterSNResult.MyViewHolder holder, int position) {
        InventoryEntity obj = listItem.get(position);

        holder.tvPallet.setText("" + obj.getPalletNo());
        holder.tvPartNo.setText("" + obj.getICTPN());
        holder.tvCTN.setText("" + obj.getCTNSN());
        holder.tvlocationNo.setText("" + obj.getLocationNo());
        holder.tvNo.setText("" + (position + 1));
        if (position % 2 == 0) {
            holder.llBodyInventorySNResult.setBackgroundColor(Color.parseColor("#FFFFFF"));
        } else
            holder.llBodyInventorySNResult.setBackgroundColor(Color.parseColor("#EFEFEF"));
    }

    @Override
    public int getItemCount() {
        return listItem.size();
    }
}

