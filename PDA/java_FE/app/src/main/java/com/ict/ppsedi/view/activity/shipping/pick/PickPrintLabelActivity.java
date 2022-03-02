package com.ict.ppsedi.view.activity.shipping.pick;

import android.app.PendingIntent;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.hardware.usb.UsbDevice;
import android.hardware.usb.UsbDeviceConnection;
import android.hardware.usb.UsbEndpoint;
import android.hardware.usb.UsbInterface;
import android.hardware.usb.UsbManager;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.text.TextUtils;
import android.view.MenuItem;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import androidx.core.app.ActivityCompat;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import com.google.zxing.BarcodeFormat;
import com.google.zxing.EncodeHintType;
import com.google.zxing.MultiFormatWriter;
import com.google.zxing.WriterException;
import com.google.zxing.common.BitMatrix;
import com.ict.ppsedi.MainActivity;
import com.ict.ppsedi.R;
import com.ict.ppsedi.bluetoothPrinterHelper.App;
import com.ict.ppsedi.bluetoothPrinterHelper.BluetoothDeviceList;
import com.ict.ppsedi.bluetoothPrinterHelper.DeviceConnFactoryManager;
import com.ict.ppsedi.bluetoothPrinterHelper.PrinterCommand;
import com.ict.ppsedi.bluetoothPrinterHelper.ThreadPool;
import com.ict.ppsedi.entities.ExecuteResult;
import com.ict.ppsedi.entities.PalletInfoEntity;
import com.ict.ppsedi.model.PickModel;
import com.ict.ppsedi.utilities.UtilsConstants;
import com.ict.ppsedi.view.BaseActivity;
import com.ict.ppsedi.view.customs.MyToast;
import com.ict.ppsedi.view.customs.VoiceManager;
import com.printer.command.EscCommand;
import com.printer.command.LabelCommand;

import java.util.EnumMap;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import java.util.Vector;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;

import static android.hardware.usb.UsbManager.ACTION_USB_DEVICE_ATTACHED;
import static android.hardware.usb.UsbManager.ACTION_USB_DEVICE_DETACHED;
import static com.ict.ppsedi.bluetoothPrinterHelper.DeviceConnFactoryManager.ACTION_QUERY_PRINTER_STATE;
import static com.ict.ppsedi.bluetoothPrinterHelper.DeviceConnFactoryManager.CONN_STATE_FAILED;

public class PickPrintLabelActivity extends BaseActivity {

    class Constant {
        public static final String SERIALPORTPATH = "SerialPortPath";
        public static final String SERIALPORTBAUDRATE = "SerialPortBaudrate";
        public static final String WIFI_CONFIG_IP = "wifi config ip";
        public static final String WIFI_CONFIG_PORT = "wifi config port";
        public static final String ACTION_USB_PERMISSION = "com.android.example.USB_PERMISSION";
        public static final int BLUETOOTH_REQUEST_CODE = 0x001;
        public static final int USB_REQUEST_CODE = 0x002;
        public static final int WIFI_REQUEST_CODE = 0x003;
        public static final int SERIALPORT_REQUEST_CODE = 0x006;
        public static final int CONN_STATE_DISCONN = 0x007;
        public static final int MESSAGE_UPDATE_PARAMETER = 0x009;

        /**
         * wifi 默认ip
         */
        public static final String WIFI_DEFAULT_IP = "192.168.123.100";

        /**
         * wifi 默认端口号
         */
        public static final int WIFI_DEFAULT_PORT = 9100;
    }


    Bitmap bitmapShipment;
    Bitmap bitmapPallet;
    private static final String ACTION_USB_PERMISSION = "com.android.example.USB_PERMISSION";

    @BindView(R.id.tvPickPallet)
    public TextView tvPickPallet;

    @BindView(R.id.tvShipment)
    public TextView tvShipment;

    @BindView(R.id.tvCarrier)
    public TextView tvCarrier;

    @BindView(R.id.tvCartonQtyInfo)
    public TextView tvCartonQtyInfo;

    @BindView(R.id.tvQty)
    public TextView tvQty;

    @BindView(R.id.tvPalletNumber)
    public TextView tvPalletNumber;

    @BindView(R.id.tvShipmentType)
    public TextView tvShipmentType;

    @BindView(R.id.tvRemark)
    public TextView tvRemark;

    @BindView(R.id.tvPalletType)
    public TextView tvPalletType;


//    private UsbManager mUsbManager;
//    private UsbDevice mDevice;
//    private UsbDeviceConnection mConnection;
//    private UsbInterface mInterface;
//    private UsbEndpoint mEndPoint;
//    private PendingIntent mPermissionIntent;
//    private static Boolean forceCLaim = true;
//    HashMap<String, UsbDevice> mDeviceList;
//    Iterator<UsbDevice> mDeviceIterator;

    public final static String INTENT_EXTRA_PARAM_PICK_BARCODE = "INTENT_EXTRA_PARAM_PICK_BARCODE";


    private ThreadPool threadPool;
    private int id = 0;
    private static final int CONN_MOST_DEVICES = 0x11;
    private String usbName;
    private TextView tvConnState;
    PalletInfoEntity pickObj;
    PickModel model;

    ImageView ivShipment;
    ImageView ivPallet;
    private VoiceManager voiceManager;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.pick_label);
        ButterKnife.bind(this);
        assert getSupportActionBar() != null;   //null check
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);   //show back button
        getSupportActionBar().setTitle("Pick Barcode");
        model = new PickModel();

        ivShipment = findViewById(R.id.ivShipment);
        ivPallet = findViewById(R.id.ivPallet);
        bindPickLabel();

        voiceManager = new VoiceManager(this);
        tvConnState = (TextView) findViewById(R.id.tv_connState);
        App.mContext = getApplicationContext();
    }


    public void bindPickLabel() {
        String objSN = (String) getIntent().getSerializableExtra(INTENT_EXTRA_PARAM_PICK_BARCODE);
        Context c = this;
        final ExecuteResult[] res = {new ExecuteResult()};
        new AsyncTask<Void, Void, String>() {
            @Override
            protected String doInBackground(Void... voids) {
                res[0] = model.getPickPalletLabelInfo(objSN);
                return "OK";
            }

            @Override
            protected void onPreExecute() {
                super.onPreExecute();
            }

            protected void onPostExecute(String result) {
                super.onPostExecute(result);
                if (res[0].isSuccess()) {
                    pickObj = new Gson().fromJson(res[0].getData().toString(), new TypeToken<PalletInfoEntity>() {
                    }.getType());
                    MyToast.show(c, "OK", UtilsConstants.Status.SUCCESS.name());

                    tvShipment.setText(pickObj.getShipmentID());
                    tvPickPallet.setText(pickObj.getPickPalletNo());

                    tvCarrier.setText(pickObj.getCarrierName());
                    tvCartonQtyInfo.setText(pickObj.getCartonQtyInfo());
                    tvQty.setText(pickObj.getQty() + "");
                    tvPalletNumber.setText(pickObj.getPalletNumber());
                    tvShipmentType.setText(pickObj.getShipmentType());
                    tvRemark.setText(pickObj.getRemark());
                    tvPalletType.setText(pickObj.getPalletType());
                    // barcode image


                    try {

                        bitmapShipment = encodeAsBitmap(pickObj.getShipmentID(), BarcodeFormat.CODE_128, 500, 100);
                        ivShipment.setImageBitmap(bitmapShipment);

                        bitmapPallet = encodeAsBitmap(pickObj.getPickPalletNo(), BarcodeFormat.CODE_128, 500, 100);
                        ivPallet.setImageBitmap(bitmapPallet);
                        voiceManager.playOK();

                    } catch (Exception e) {
                    }
                } else {
                    MyToast.show(c, res[0].getMessage(), UtilsConstants.Status.ERROR.name());
                    voiceManager.playNG();
                }
            }
        }.execute();

    }


    @Override
    protected void onStart() {
        super.onStart();
        IntentFilter filter = new IntentFilter(ACTION_USB_PERMISSION);
        filter.addAction(ACTION_USB_DEVICE_DETACHED);
        filter.addAction(ACTION_QUERY_PRINTER_STATE);
        filter.addAction(DeviceConnFactoryManager.ACTION_CONN_STATE);
        filter.addAction(ACTION_USB_DEVICE_ATTACHED);
        registerReceiver(receiver, filter);
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        Toast.makeText(this, "resultCode: " + resultCode, Toast.LENGTH_SHORT).show();
        if (resultCode == RESULT_OK) {
            switch (requestCode) {
                /*蓝牙连接*/
                case Constant.BLUETOOTH_REQUEST_CODE: {
                    Toast.makeText(this, "Bluetooth request", Toast.LENGTH_SHORT).show();
//                    closeport();
                    /*获取蓝牙mac地址*/
                    String macAddress = data.getStringExtra(BluetoothDeviceList.EXTRA_DEVICE_ADDRESS);
                    /* 初始化话DeviceConnFactoryManager */
                    new DeviceConnFactoryManager.Build()
                            .setId(id)
                            /* 设置连接方式 */
                            .setConnMethod(DeviceConnFactoryManager.CONN_METHOD.BLUETOOTH)
                            /* 设置连接的蓝牙mac地址 */
                            .setMacAddress(macAddress)
                            .build();
                    /* 打开端口 */
                    threadPool = ThreadPool.getInstantiation();
                    threadPool.addTask(new Runnable() {
                        @Override
                        public void run() {
                            DeviceConnFactoryManager.getDeviceConnFactoryManagers()[id].openPort();
                        }
                    });

                    break;
                }
                case Constant.SERIALPORT_REQUEST_CODE:
                    closeport();
                    /* 获取波特率 */
                    int baudrate = data.getIntExtra(Constant.SERIALPORTBAUDRATE, 0);
                    /* 获取串口号 */
                    String path = data.getStringExtra(Constant.SERIALPORTPATH);

                    if (baudrate != 0 && !TextUtils.isEmpty(path)) {
                        /* 初始化DeviceConnFactoryManager */
                        new DeviceConnFactoryManager.Build()
                                /* 设置连接方式 */
                                .setConnMethod(DeviceConnFactoryManager.CONN_METHOD.SERIAL_PORT)
                                .setId(id)
                                /* 设置波特率 */
                                .setBaudrate(baudrate)
                                /* 设置串口号 */
                                .setSerialPort(path)
                                .build();
                        /* 打开端口 */
                        DeviceConnFactoryManager.getDeviceConnFactoryManagers()[id].openPort();
                    }
                    break;
                case CONN_MOST_DEVICES:
                    id = data.getIntExtra("id", -1);
                    if (DeviceConnFactoryManager.getDeviceConnFactoryManagers()[id] != null &&
                            DeviceConnFactoryManager.getDeviceConnFactoryManagers()[id].getConnState()) {
                        tvConnState.setText(getString(R.string.str_conn_state_connected) + "\n" + getConnDeviceInfo());
                    } else {
                        tvConnState.setText(getString(R.string.str_conn_state_disconnect));
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private String getConnDeviceInfo() {
        String str = "";
        DeviceConnFactoryManager deviceConnFactoryManager = DeviceConnFactoryManager.getDeviceConnFactoryManagers()[id];
        if (deviceConnFactoryManager != null
                && deviceConnFactoryManager.getConnState()) {
            if ("USB".equals(deviceConnFactoryManager.getConnMethod().toString())) {
                str += "USB\n";
                str += "USB Name: " + deviceConnFactoryManager.usbDevice().getDeviceName();
            } else if ("WIFI".equals(deviceConnFactoryManager.getConnMethod().toString())) {
                str += "WIFI\n";
                str += "IP: " + deviceConnFactoryManager.getIp() + "\t";
                str += "Port: " + deviceConnFactoryManager.getPort();
            } else if ("BLUETOOTH".equals(deviceConnFactoryManager.getConnMethod().toString())) {
                str += "BLUETOOTH\n";
                str += "MacAddress: " + deviceConnFactoryManager.getMacAddress();
            } else if ("SERIAL_PORT".equals(deviceConnFactoryManager.getConnMethod().toString())) {
                str += "SERIAL_PORT\n";
                str += "Path: " + deviceConnFactoryManager.getSerialPortPath() + "\t";
                str += "Baudrate: " + deviceConnFactoryManager.getBaudrate();
            }
        }
        Toast.makeText(this, "device: " + str, Toast.LENGTH_LONG).show();
        return (str);
    }

    private void closeport() {
        if (DeviceConnFactoryManager.getDeviceConnFactoryManagers()[id] != null && DeviceConnFactoryManager.getDeviceConnFactoryManagers()[id].mPort != null) {
            DeviceConnFactoryManager.getDeviceConnFactoryManagers()[id].reader.cancel();
            DeviceConnFactoryManager.getDeviceConnFactoryManagers()[id].mPort.closePort();
            DeviceConnFactoryManager.getDeviceConnFactoryManagers()[id].mPort = null;
        }
    }

    public void btnBluetoothConn(View view) {
        startActivityForResult(new Intent(this, BluetoothDeviceList.class), Constant.BLUETOOTH_REQUEST_CODE);
    }

    public void btnLabelPrint(View view) {
        threadPool = ThreadPool.getInstantiation();
        threadPool.addTask(new Runnable() {
            @Override
            public void run() {
                if (DeviceConnFactoryManager.getDeviceConnFactoryManagers()[id] == null ||
                        !DeviceConnFactoryManager.getDeviceConnFactoryManagers()[id].getConnState()) {
                    mHandler.obtainMessage(CONN_PRINTER).sendToTarget();
                    return;
                }
                if (DeviceConnFactoryManager.getDeviceConnFactoryManagers()[id].getCurrentPrinterCommand() == PrinterCommand.TSC) {
                    sendLabel2();
                } else {
                    mHandler.obtainMessage(PRINTER_COMMAND_ERROR).sendToTarget();
                }
            }
        });
    }

    void sendLabel() {
        LabelCommand tsc = new LabelCommand();
        /* 撕纸模式开启 */
        tsc.addTear(EscCommand.ENABLE.ON);
        /* 设置标签尺寸，按照实际尺寸设置 */
        tsc.addSize(70, 52);
        /* 设置标签间隙，按照实际尺寸设置，如果为无间隙纸则设置为0 */
        tsc.addGap(0);
        /* 设置打印方向 */
        tsc.addDirection(LabelCommand.DIRECTION.FORWARD, LabelCommand.MIRROR.NORMAL);
        /* 开启带Response的打印，用于连续打印 */
        tsc.addQueryPrinterStatus(LabelCommand.RESPONSE_MODE.ON);
        /* 设置原点坐标 */
        tsc.addReference(0, 0);
        /* 清除打印缓冲区 */
        tsc.addCls();
        /* 绘制简体中文 */
        tsc.addText(10, 50, LabelCommand.FONTTYPE.SIMPLIFIED_CHINESE, LabelCommand.ROTATION.ROTATION_0, LabelCommand.FONTMUL.MUL_1, LabelCommand.FONTMUL.MUL_1,
                "Welcome to use our printer");
        /* 绘制图片 */
        Bitmap b = BitmapFactory.decodeResource(getResources(), R.drawable.ic_baseline_arrow_drop_up_24);
        tsc.addBitmap(10, 20, LabelCommand.BITMAP_MODE.OVERWRITE, 300, b);

        tsc.addQRCode(10, 330, LabelCommand.EEC.LEVEL_L, 5, LabelCommand.ROTATION.ROTATION_0, "Printer");
        /* 绘制一维条码 */
        tsc.add1DBarcode(10, 450, LabelCommand.BARCODETYPE.CODE128, 100, LabelCommand.READABEL.EANBEL, LabelCommand.ROTATION.ROTATION_0, "SMARNET");

        tsc.addText(10, 580, LabelCommand.FONTTYPE.SIMPLIFIED_CHINESE, LabelCommand.ROTATION.ROTATION_0, LabelCommand.FONTMUL.MUL_1, LabelCommand.FONTMUL.MUL_1,
                "简体字");

        tsc.addText(100, 580, LabelCommand.FONTTYPE.TRADITIONAL_CHINESE, LabelCommand.ROTATION.ROTATION_0, LabelCommand.FONTMUL.MUL_1, LabelCommand.FONTMUL.MUL_1,
                "繁體字");

        tsc.addText(190, 580, LabelCommand.FONTTYPE.KOREAN, LabelCommand.ROTATION.ROTATION_0, LabelCommand.FONTMUL.MUL_1, LabelCommand.FONTMUL.MUL_1,
                "한국어");

        /* 打印标签 */
        tsc.addPrint(1, 1);
        /* 打印标签后 蜂鸣器响 */

        tsc.addSound(2, 100);
        tsc.addCashdrwer(LabelCommand.FOOT.F5, 255, 255);
        Vector<Byte> datas = tsc.getCommand();
        /* 发送数据 */
        if (DeviceConnFactoryManager.getDeviceConnFactoryManagers()[id] == null) {
            return;
        }
        DeviceConnFactoryManager.getDeviceConnFactoryManagers()[id].sendDataImmediately(datas);
    }

    void sendLabel2() {
        LabelCommand tsc = new LabelCommand();
        /* 撕纸模式开启 */
        tsc.addTear(EscCommand.ENABLE.ON);
        /* 设置标签尺寸，按照实际尺寸设置 */
        tsc.addSize(70, 52);
        /* 设置标签间隙，按照实际尺寸设置，如果为无间隙纸则设置为0 */
        tsc.addGap(0);
        /* 设置打印方向 */
        tsc.addDirection(LabelCommand.DIRECTION.FORWARD, LabelCommand.MIRROR.NORMAL);
        /* 开启带Response的打印，用于连续打印 */
        tsc.addQueryPrinterStatus(LabelCommand.RESPONSE_MODE.ON);
        /* 设置原点坐标 */
        tsc.addReference(0, 0);
        /* 清除打印缓冲区 */
        tsc.addCls();
        /* 绘制简体中文 */

        tsc.addText(190, 20, LabelCommand.FONTTYPE.FONT_3, LabelCommand.ROTATION.ROTATION_0, LabelCommand.FONTMUL.MUL_1, LabelCommand.FONTMUL.MUL_1,
                "Pick Pallet Label");

        tsc.addText(10, 50, LabelCommand.FONTTYPE.SIMPLIFIED_CHINESE, LabelCommand.ROTATION.ROTATION_0, LabelCommand.FONTMUL.MUL_1, LabelCommand.FONTMUL.MUL_1,
                "Shipment ID:");

        tsc.add1DBarcode(200, 50, LabelCommand.BARCODETYPE.CODE128, 40, LabelCommand.READABEL.EANBEL, LabelCommand.ROTATION.ROTATION_0, pickObj.getShipmentID());

        tsc.addText(10, 140, LabelCommand.FONTTYPE.SIMPLIFIED_CHINESE, LabelCommand.ROTATION.ROTATION_0, LabelCommand.FONTMUL.MUL_1, LabelCommand.FONTMUL.MUL_1,
                "Pick Pallet ID:");

        tsc.add1DBarcode(200, 140, LabelCommand.BARCODETYPE.CODE128, 40, LabelCommand.READABEL.EANBEL, LabelCommand.ROTATION.ROTATION_0, pickObj.getPickPalletNo());

        tsc.addText(10, 220, LabelCommand.FONTTYPE.SIMPLIFIED_CHINESE, LabelCommand.ROTATION.ROTATION_0, LabelCommand.FONTMUL.MUL_1, LabelCommand.FONTMUL.MUL_1,
                "Carrier:");
        tsc.addText(110, 220, LabelCommand.FONTTYPE.SIMPLIFIED_CHINESE, LabelCommand.ROTATION.ROTATION_0, LabelCommand.FONTMUL.MUL_1, LabelCommand.FONTMUL.MUL_1,
                pickObj.getCarrierName());

        tsc.addText(200, 220, LabelCommand.FONTTYPE.SIMPLIFIED_CHINESE, LabelCommand.ROTATION.ROTATION_0, LabelCommand.FONTMUL.MUL_1, LabelCommand.FONTMUL.MUL_1,
                "Total cartons:");
        tsc.addText(400, 220, LabelCommand.FONTTYPE.SIMPLIFIED_CHINESE, LabelCommand.ROTATION.ROTATION_0, LabelCommand.FONTMUL.MUL_1, LabelCommand.FONTMUL.MUL_1,
                pickObj.getCartonQtyInfo());

        tsc.addText(10, 260, LabelCommand.FONTTYPE.SIMPLIFIED_CHINESE, LabelCommand.ROTATION.ROTATION_0, LabelCommand.FONTMUL.MUL_1, LabelCommand.FONTMUL.MUL_1,
                "Qty:");
        tsc.addText(60, 260, LabelCommand.FONTTYPE.SIMPLIFIED_CHINESE, LabelCommand.ROTATION.ROTATION_0, LabelCommand.FONTMUL.MUL_1, LabelCommand.FONTMUL.MUL_1,
                pickObj.getQty() + "");

        tsc.addText(200, 260, LabelCommand.FONTTYPE.SIMPLIFIED_CHINESE, LabelCommand.ROTATION.ROTATION_0, LabelCommand.FONTMUL.MUL_1, LabelCommand.FONTMUL.MUL_1,
                "Total Pallets:");
        tsc.addText(400, 260, LabelCommand.FONTTYPE.SIMPLIFIED_CHINESE, LabelCommand.ROTATION.ROTATION_0, LabelCommand.FONTMUL.MUL_1, LabelCommand.FONTMUL.MUL_1,
                pickObj.getPalletNumber());

        tsc.addText(10, 300, LabelCommand.FONTTYPE.SIMPLIFIED_CHINESE, LabelCommand.ROTATION.ROTATION_0, LabelCommand.FONTMUL.MUL_1, LabelCommand.FONTMUL.MUL_1,
                pickObj.getShipmentType());
        tsc.addText(200, 300, LabelCommand.FONTTYPE.SIMPLIFIED_CHINESE, LabelCommand.ROTATION.ROTATION_0, LabelCommand.FONTMUL.MUL_1, LabelCommand.FONTMUL.MUL_1,
                pickObj.getPalletType());

        tsc.addText(200, 340, LabelCommand.FONTTYPE.SIMPLIFIED_CHINESE, LabelCommand.ROTATION.ROTATION_0, LabelCommand.FONTMUL.MUL_1, LabelCommand.FONTMUL.MUL_1,
                pickObj.getRemark());
        /* 打印标签 */
        tsc.addPrint(1, 1);
        /* 打印标签后 蜂鸣器响 */

        tsc.addSound(2, 100);
        tsc.addCashdrwer(LabelCommand.FOOT.F5, 255, 255);
        Vector<Byte> datas = tsc.getCommand();
        /* 发送数据 */
        if (DeviceConnFactoryManager.getDeviceConnFactoryManagers()[id] == null) {
            return;
        }
        DeviceConnFactoryManager.getDeviceConnFactoryManagers()[id].sendDataImmediately(datas);
    }

    private static final int CONN_STATE_DISCONN = 0x007;
    private static final int PRINTER_COMMAND_ERROR = 0x008;
    private static final int CONN_PRINTER = 0x12;


    private BroadcastReceiver receiver = new BroadcastReceiver() {
        @Override
        public void onReceive(Context context, Intent intent) {
            String action = intent.getAction();
            switch (action) {
                case DeviceConnFactoryManager.ACTION_CONN_STATE:
                    int state = intent.getIntExtra(DeviceConnFactoryManager.STATE, -1);
                    int deviceId = intent.getIntExtra(DeviceConnFactoryManager.DEVICE_ID, -1);
                    switch (state) {
                        case DeviceConnFactoryManager.CONN_STATE_DISCONNECT:
                            if (id == deviceId) {
                                tvConnState.setText(getString(R.string.str_conn_state_disconnect));
                            }
                            break;
                        case DeviceConnFactoryManager.CONN_STATE_CONNECTING:
                            tvConnState.setText(getString(R.string.str_conn_state_connecting));
                            break;
                        case DeviceConnFactoryManager.CONN_STATE_CONNECTED:
                            tvConnState.setText(getString(R.string.str_conn_state_connected) + "\n" + getConnDeviceInfo());
                            break;
                        case CONN_STATE_FAILED:
                            Toast.makeText(PickPrintLabelActivity.this, getString(R.string.str_conn_fail), Toast.LENGTH_SHORT).show();
                            tvConnState.setText(getString(R.string.str_conn_state_disconnect));
                            break;
                        default:
                            break;
                    }
                    break;
                case ACTION_QUERY_PRINTER_STATE:
                    int a1 = 0;
//                    if (counts >= 0) {
//                        if (continuityprint) {
//                            printcount++;
//                            Utils.toast(MainActivity2.this, getString(R.string.str_continuityprinter) + " " + printcount);
//                        }
//                        if (counts != 0) {
//                            sendContinuityPrint();
//                        } else {
//                            continuityprint = false;
//                        }
//                    }
                    break;
                default:
                    break;
            }
        }
    };
    private Handler mHandler = new Handler() {
        @Override
        public void handleMessage(Message msg) {
            switch (msg.what) {
                case CONN_STATE_DISCONN:
                    if (DeviceConnFactoryManager.getDeviceConnFactoryManagers()[id] != null || !DeviceConnFactoryManager.getDeviceConnFactoryManagers()[id].getConnState()) {
                        DeviceConnFactoryManager.getDeviceConnFactoryManagers()[id].closePort(id);
                        Toast.makeText(PickPrintLabelActivity.this, getString(R.string.str_disconnect_success), Toast.LENGTH_SHORT).show();
                    }
                    break;
                case PRINTER_COMMAND_ERROR:
                    Toast.makeText(PickPrintLabelActivity.this, getString(R.string.str_choice_printer_command), Toast.LENGTH_SHORT).show();
                    break;
                case CONN_PRINTER:
                    Toast.makeText(PickPrintLabelActivity.this, getString(R.string.str_cann_printer), Toast.LENGTH_SHORT).show();
                    break;
                case Constant.MESSAGE_UPDATE_PARAMETER:
                    String strIp = msg.getData().getString("Ip");
                    String strPort = msg.getData().getString("Port");
                    /* 初始化端口信息 */
                    new DeviceConnFactoryManager.Build()
                            /* 设置端口连接方式 */
                            .setConnMethod(DeviceConnFactoryManager.CONN_METHOD.WIFI)
                            /* 设置端口IP地址 */
                            .setIp(strIp)
                            /* 设置端口ID（主要用于连接多设备） */
                            .setId(id)
                            /* 设置连接的热点端口号 */
                            .setPort(Integer.parseInt(strPort))
                            .build();
                    threadPool = ThreadPool.getInstantiation();
                    threadPool.addTask(new Runnable() {
                        @Override
                        public void run() {
                            DeviceConnFactoryManager.getDeviceConnFactoryManagers()[id].openPort();
                        }
                    });
                    break;
                default:
                    new DeviceConnFactoryManager.Build()
                            /* 设置端口连接方式 */
                            .setConnMethod(DeviceConnFactoryManager.CONN_METHOD.WIFI)
                            /* 设置端口IP地址 */
                            .setIp("192.168.2.227")
                            /* 设置端口ID（主要用于连接多设备） */
                            .setId(id)
                            /* 设置连接的热点端口号 */
                            .setPort(9100)
                            .build();
                    threadPool.addTask(new Runnable() {
                        @Override
                        public void run() {
                            DeviceConnFactoryManager.getDeviceConnFactoryManagers()[id].openPort();
                        }
                    });
                    break;
            }
        }
    };


    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case android.R.id.home:
                backpress1();
                return true;
        }
        return super.onOptionsItemSelected(item);
    }

    public void backpress1() {
//        Intent moveResult = new Intent();
//        this.setResult(Activity.RESULT_OK, moveResult);
//        this.finish();

        Intent intent = new Intent(PickPrintLabelActivity.this,
                MainActivity.class);
        intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP
                | Intent.FLAG_ACTIVITY_SINGLE_TOP);
        startActivity(intent);
        DeviceConnFactoryManager.getDeviceConnFactoryManagers()[0].closeAllPort();
//        ActivityCompat.finishAffinity(PickPrintLabelActivity.this);
    }

    @Override
    public void onBackPressed() {
        Intent intent = new Intent(PickPrintLabelActivity.this,
                MainActivity.class);
        intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP
                | Intent.FLAG_ACTIVITY_SINGLE_TOP);
        startActivity(intent);
    }

    @OnClick(R.id.btnPrint)
    public void btnPrint_OnClick() {
        printGprinter();
    }

    public void printGprinter() {
    }


//    public void btnPrint() {
//        final BroadcastReceiver mUsbReceiver = new BroadcastReceiver() {
//            @Override
//            public void onReceive(Context context, Intent intent) {
//                String action = intent.getAction();
//                if (ACTION_USB_PERMISSION.equals(action)) {
//                    synchronized (this) {
//                        UsbDevice device = (UsbDevice) intent.getParcelableExtra(UsbManager.EXTRA_DEVICE);
//
//                        if (intent.getBooleanExtra(UsbManager.EXTRA_PERMISSION_GRANTED, false)) {
//                            if (device != null) {
//                                //call method to set up device communication
//                                Toast.makeText(context, "device: " + device.getDeviceName(), Toast.LENGTH_SHORT).show();
//                                //setup();
//                            }
//                        } else {
//                            //Log.d("SUB", "permission denied for device " + device);
//                            Toast.makeText(context, "PERMISSION DENIED FOR THIS DEVICE", Toast.LENGTH_SHORT).show();
//                        }
//                    }
//                }
//            }
//        };
//    }


    private static final int WHITE = 0xFFFFFFFF;
    private static final int BLACK = 0xFF000000;

    Bitmap encodeAsBitmap(String contents, BarcodeFormat format, int img_width, int img_height) throws WriterException {
        String contentsToEncode = contents;
        if (contentsToEncode == null) {
            return null;
        }
        Map<EncodeHintType, Object> hints = null;
        String encoding = guessAppropriateEncoding(contentsToEncode);
        if (encoding != null) {
            hints = new EnumMap<>(EncodeHintType.class);
            hints.put(EncodeHintType.CHARACTER_SET, encoding);
        }
        MultiFormatWriter writer = new MultiFormatWriter();
        BitMatrix result;
        try {
            result = writer.encode(contentsToEncode, format, img_width, img_height, hints);
        } catch (IllegalArgumentException iae) {
            // Unsupported format
            return null;
        }
        int width = result.getWidth();
        int height = result.getHeight();
        int[] pixels = new int[width * height];
        for (int y = 0; y < height; y++) {
            int offset = y * width;
            for (int x = 0; x < width; x++) {
                pixels[offset + x] = result.get(x, y) ? BLACK : WHITE;
            }
        }

        Bitmap bitmap = Bitmap.createBitmap(width, height,
                Bitmap.Config.ARGB_8888);
        bitmap.setPixels(pixels, 0, width, 0, 0, width, height);
        return bitmap;
    }

    private static String guessAppropriateEncoding(CharSequence contents) {
        // Very crude at the moment
        for (int i = 0; i < contents.length(); i++) {
            if (contents.charAt(i) > 0xFF) {
                return "UTF-8";
            }
        }
        return null;
    }

}
