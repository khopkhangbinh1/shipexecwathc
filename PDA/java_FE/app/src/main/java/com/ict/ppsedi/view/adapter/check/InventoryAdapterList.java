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
import android.content.Context;
import android.graphics.Color;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.LinearLayout;
import android.widget.TextView;

import androidx.recyclerview.widget.RecyclerView;

import com.ict.ppsedi.R;
import com.ict.ppsedi.entities.PickLogEntity;
import com.ict.ppsedi.entities.PickPartLocationEntity;

import java.util.ArrayList;

import java.util.ArrayList;

public class InventoryAdapterList extends RecyclerView.Adapter<InventoryAdapterList.MyViewHolder> {
    private Context mContext;
    private ArrayList<InventoryEntity> listItem;
    private LayoutInflater mLayoutInflater;

    public interface ItemClickListener {
        void onClick(View view, int position, boolean isLongClick);
    }


    public InventoryAdapterList(Context context, ArrayList<InventoryEntity> array1) {
        // TODO Auto-generated constructor stub
        this.listItem = array1;
        mLayoutInflater = LayoutInflater.from(context);
        this.mContext = context;
    }


    protected class MyViewHolder extends RecyclerView.ViewHolder implements InventoryAdapterList.ItemClickListener {

        public TextView tvPartNo, tvPallet, tvCatonQty, tvQty, tvQhold, QHQty;
        LinearLayout llBodyInventory;
        InventoryAdapterList.ItemClickListener itemClickListener;

        public MyViewHolder(View v) {
            super(v);
            llBodyInventory = v.findViewById(R.id.llBodyInventory);

            tvPartNo = v.findViewById(R.id.tvPartNo);
            tvPallet = v.findViewById(R.id.tvPallet);
//            tvCatonQty = v.findViewById(R.id.tvCatonQty);
            tvQty = v.findViewById(R.id.tvQty);
//            tvQhold = v.findViewById(R.id.tvQhold);
            QHQty = v.findViewById(R.id.QHQty);
        }

        public void setItemClickListener(InventoryAdapterList.ItemClickListener itemClickListener) {
            this.itemClickListener = itemClickListener;
        }


        @Override
        public void onClick(View view, int position, boolean isLongClick) {
            itemClickListener.onClick(view, getAdapterPosition(), false);
        }
    }

    @Override
    public MyViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View item = mLayoutInflater.inflate(R.layout.inventory_location_item, parent, false);
        return new InventoryAdapterList.MyViewHolder(item);
    }

    @Override
    public void onBindViewHolder(InventoryAdapterList.MyViewHolder holder, int position) {
        InventoryEntity obj = listItem.get(position);

        holder.tvPartNo.setText(obj.getICTPN());
        holder.tvPallet.setText(obj.getPalletNo());
//        holder.tvCatonQty.setText(obj.getCartonQTY());
        holder.tvQty.setText(obj.getQty()+"/"+obj.getCartonQTY());
//        holder.tvQhold.setText(obj.getQHCaton());
        holder.QHQty.setText(obj.getQHQty()+"/"+obj.getQHCaton());




        if (position % 2 == 0) {
            holder.llBodyInventory.setBackgroundColor(Color.parseColor("#FFFFFF"));
        } else
            holder.llBodyInventory.setBackgroundColor(Color.parseColor("#EFEFEF"));
    }

    @Override
    public int getItemCount() {
        return listItem.size();
    }
}

