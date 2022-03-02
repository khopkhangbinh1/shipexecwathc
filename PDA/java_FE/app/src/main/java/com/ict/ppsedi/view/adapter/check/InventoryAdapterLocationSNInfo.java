package com.ict.ppsedi.view.adapter.check;

import android.content.Context;
import android.graphics.Color;
import android.view.View;
import android.view.ViewGroup;
import android.view.LayoutInflater;
import android.widget.BaseAdapter;
import android.widget.LinearLayout;
import android.widget.TextView;

import androidx.recyclerview.widget.RecyclerView;

import com.ict.ppsedi.R;
import com.ict.ppsedi.entities.InventoryEntity;
import com.ict.ppsedi.view.fragment.edi.check.InventoryFragment;

import java.util.ArrayList;
import java.util.List;

public class InventoryAdapterLocationSNInfo extends RecyclerView.Adapter<InventoryAdapterLocationSNInfo.MyViewHolder> {
    private Context mContext;
    private ArrayList<InventoryEntity> listItem;
    private LayoutInflater mLayoutInflater;
    private static ArrayList<InventoryEntity> lstDone;

    public interface ItemClickListener {
        void onClick(View view, int position, boolean isLongClick);
    }

    public static void setListSNDone(ArrayList<InventoryEntity> _lstDone) {
        int a = 0;
        lstDone = new ArrayList<>();
        lstDone.addAll(_lstDone);
    }

    public InventoryAdapterLocationSNInfo(Context context, ArrayList<InventoryEntity> array1) {
        // TODO Auto-generated constructor stub
        this.listItem = array1;
        mLayoutInflater = LayoutInflater.from(context);
        this.mContext = context;
        if (lstDone == null)
            lstDone = new ArrayList<>();
    }


    protected class MyViewHolder extends RecyclerView.ViewHolder implements com.ict.ppsedi.view.adapter.check.InventoryAdapterLocationSNInfo.ItemClickListener {

        public TextView tvPallet, tvPartNo, tvCTN, tvNo;//,tvlocationNo;
        LinearLayout llBodyInventoryLocationSN;
        InventoryAdapterCheckLog.ItemClickListener itemClickListener;

        public MyViewHolder(View v) {
            super(v);
            llBodyInventoryLocationSN = v.findViewById(R.id.llBodyInventoryLocationSN);
            tvPallet = v.findViewById(R.id.tvPallet);
            tvCTN = v.findViewById(R.id.tvCTN);
            tvPartNo = v.findViewById(R.id.tvPartNo);
            tvNo = v.findViewById(R.id.tvNo);

//            tvlocationNo = v.findViewById(R.id.tvlocationNo);
        }

        public void setItemClickListener(InventoryAdapterCheckLog.ItemClickListener itemClickListener) {
            this.itemClickListener = itemClickListener;
        }


        @Override
        public void onClick(View view, int position, boolean isLongClick) {
            itemClickListener.onClick(view, getAdapterPosition(), false);
        }
    }

    @Override
    public InventoryAdapterLocationSNInfo.MyViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View item = mLayoutInflater.inflate(R.layout.inventory_location_sn_info, parent, false);
        return new InventoryAdapterLocationSNInfo.MyViewHolder(item);
    }

    @Override
    public void onBindViewHolder(InventoryAdapterLocationSNInfo.MyViewHolder holder, int position) {
        InventoryEntity obj = listItem.get(position);


        holder.tvPallet.setText(obj.getPalletNo());
        holder.tvPartNo.setText(obj.getICTPN());
        holder.tvCTN.setText(obj.getCTNSN());
        holder.tvNo.setText("" + (position + 1));
        //        holder.tvlocationNo.setText("" + obj.getLocationNo());


        if (lstDone.stream().filter(x -> x.getCTNSN().equals(obj.getCTNSN())).count() > 0) {
            holder.llBodyInventoryLocationSN.setBackgroundColor(Color.parseColor("#dce625"));
        } else {
            holder.llBodyInventoryLocationSN.setBackgroundColor(Color.parseColor("#FFFFFF"));
        }
        String c = obj.getCTNSN();
        int a = 1;
//        else {
//            if (position % 2 == 0) {
//                holder.llBodyInventoryLocationSN.setBackgroundColor(Color.parseColor("#FFFFFF"));
//            } else
//                holder.llBodyInventoryLocationSN.setBackgroundColor(Color.parseColor("#EFEFEF"));
//        }
    }

    @Override
    public int getItemCount() {
        return listItem.size();
    }
}
