package com.ict.ppsedi.view.adapter.pick;

import android.content.Context;
import android.graphics.Color;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ImageButton;
import android.widget.LinearLayout;
import android.widget.TextView;

import androidx.recyclerview.widget.RecyclerView;

import com.ict.ppsedi.R;
import com.ict.ppsedi.entities.PalletInfoEntity;
import com.ict.ppsedi.entities.T_SHIPMENT_PALLET_EXT;
import com.ict.ppsedi.presenter.shipping.pick.PickSeachPresenter;

import java.util.ArrayList;

public class PickSearchAdapter extends RecyclerView.Adapter<PickSearchAdapter.MyViewHolder> {

    private Context mContext;
    private ArrayList<PalletInfoEntity> listItem;
    //    private ArrayList<T_SHIPMENT_PALLET_EXT> listItem;
    private LayoutInflater mLayoutInflater;
    PickSeachPresenter presenter;
    int checkPos = -1;

    public interface ItemClickListener {
        void onClick(View view, int position, boolean isLongClick);
    }


    public PickSearchAdapter(Context context, ArrayList<PalletInfoEntity> array1, PickSeachPresenter _presenter) {
        // TODO Auto-generated constructor stub
        this.listItem = array1;
        mLayoutInflater = LayoutInflater.from(context);
        this.mContext = context;
        this.presenter = _presenter;
    }


    protected class MyViewHolder extends RecyclerView.ViewHolder implements ItemClickListener {

        public TextView tvNo, tvShipment, tvPallet, tvCarrier, tvRegion;
        LinearLayout llBody;
        ItemClickListener itemClickListener;

        public MyViewHolder(View v) {
            super(v);
            llBody = v.findViewById(R.id.llBody);
            tvNo = v.findViewById(R.id.tvNo);
            tvShipment = v.findViewById(R.id.tvShipment);
            tvPallet = v.findViewById(R.id.tvPallet);
            tvCarrier = v.findViewById(R.id.tvCarrier);
            tvRegion = v.findViewById(R.id.tvRegion);
        }

        public void setItemClickListener(ItemClickListener itemClickListener) {
            this.itemClickListener = itemClickListener;
        }


        @Override
        public void onClick(View view, int position, boolean isLongClick) {
            itemClickListener.onClick(view, getAdapterPosition(), false);
        }
    }

    @Override
    public MyViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View item = mLayoutInflater.inflate(R.layout.pick_search_list_item, parent, false);
        return new MyViewHolder(item);
    }

    @Override
    public void onBindViewHolder(PickSearchAdapter.MyViewHolder holder, int position) {
        PalletInfoEntity obj = listItem.get(position);

        holder.tvNo.setText("" + (position + 1));
        holder.tvShipment.setText("" + obj.getShipmentID());
        holder.tvPallet.setText("" + obj.getPalletNo());
        if (obj.getCarrierName().length() <= 6)
            holder.tvCarrier.setText(obj.getCarrierName() + "\n" + obj.getShipType());
        else
            holder.tvCarrier.setText(obj.getCarrierName().substring(0, 6) + "...\n" + obj.getShipType());

        holder.tvRegion.setText(obj.getRegion() + "\n" + obj.getPalletType());

        holder.llBody.setOnLongClickListener(v -> {
            presenter.navigationDetail(obj);
            return true;
        });

        holder.llBody.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                checkPos = position;
                notifyDataSetChanged();
            }
        });

        if (!obj.getPickStatus().equals("WP")) {
            holder.llBody.setBackgroundColor(Color.parseColor("#c1b80f"));
        } else {
            if (position % 2 == 0) {
                holder.llBody.setBackgroundColor(Color.parseColor("#EFEFEF"));
            } else
                holder.llBody.setBackgroundColor(Color.parseColor("#FFFFFF"));
        }
        holder.setIsRecyclable(false);
        if (checkPos == position)
            holder.llBody.setBackgroundColor(Color.parseColor("#0095ff"));
    }

    @Override
    public int getItemCount() {
        return listItem.size();
    }

}
