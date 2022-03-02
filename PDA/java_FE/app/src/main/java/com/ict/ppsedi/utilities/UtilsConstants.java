package com.ict.ppsedi.utilities;

public class UtilsConstants {
    public enum Status {
        SUCCESS("s"),
        WARNING("w"),
        ERROR("e");
        private final String name;

        Status(String s) {
            name = s;
        }
    }

    final public static int KEY_SCAN_PDA = 288;
    //    final public static String WEB_API_ROOT = "http://10.177.64.171:8082";
    //final public static String WEB_API_ROOT = "http://10.177.20.4";
    final public static String WEB_API_ROOT = "http://10.177.19.3";
}

