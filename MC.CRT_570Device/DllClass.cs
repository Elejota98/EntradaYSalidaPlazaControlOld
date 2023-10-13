using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MC.CRT_570Device
{
    public class DllClass
    {
        //打开串口
        [DllImport("CRT_570.dll")]
        public static extern UInt32 CommOpen(string port);
        //按指定的波特率打开串口
        [DllImport("CRT_570.dll")]
        public static extern long CommOpenWithBaut(string port, byte _BaudOption);
        //关闭串口
        [DllImport("CRT_570.dll")]
        public static extern int CommClose(UInt32 ComHandle);


        [DllImport("CRT_570.dll")]
        public static extern int CRT570_Reset(UInt32 ComHandle);

        [DllImport("CRT_570.dll")]
        public static extern int CRT570_Dispense(UInt32 ComHandle);

        [DllImport("CRT_570.dll")]
        public static extern int CRT570_Capture(UInt32 ComHandle);

        [DllImport("CRT_570.dll")]
        public static extern int CRT570_PreDispense(UInt32 ComHandle);

        [DllImport("CRT_570.dll")]
        public static extern int CRT570_GetRF(UInt32 ComHandle, ref byte _PSS1, ref byte _PSS2, ref byte _PSS3);

        [DllImport("CRT_570.dll")]
        public static extern int CRT570_GetAP(UInt32 ComHandle, ref byte _PSS1, ref byte _PSS2, ref byte _PSS3, ref byte _PSS4);

        [DllImport("CRT_570.dll")]
        public static extern int CRT570_IN(UInt32 ComHandle, byte _CardIn);

        [DllImport("CRT_570.dll")]
        public static extern int CRT570_SI(UInt32 ComHandle, ref byte _CardIn);

        [DllImport("CRT_570.dll")]
        public static extern int CRT_R_DetectCard(UInt32 ComHandle, ref byte _CardType, ref byte _CardInfor);





        [DllImport("CRT_570.dll")]
        public static extern int RF_DetectCard(UInt32 ComHandle);

        [DllImport("CRT_570.dll")]
        public static extern int RF_GetCardID(UInt32 ComHandle, ref byte _CardIDLen, byte[] _CardID);

        [DllImport("CRT_570.dll")]
        public static extern int RF_LoadSecKey(UInt32 ComHandle, byte _Sec, byte _KEYType, byte _KEYLen, byte[] _KEY);

        [DllImport("CRT_570.dll")]
        public static extern int RF_ChangeSecKey(UInt32 ComHandle, byte _Sec, byte _KEYLen, byte[] _KEY);

        [DllImport("CRT_570.dll")]
        public static extern int RF_ReadBlock(UInt32 ComHandle, byte _Sec, byte _Block, ref byte _BlockDataLen, byte[] _BlockData);

        [DllImport("CRT_570.dll")]
        public static extern int RF_WriteBlock(UInt32 ComHandle, byte _Sec, byte _Block, byte _BlockDataLen, byte[] _BlockData);

        [DllImport("CRT_570.dll")]
        public static extern int RF_InitValue(UInt32 ComHandle, byte _Sec, byte _Block, byte _DataLen, byte[] _Data);

        [DllImport("CRT_570.dll")]
        public static extern int RF_ReadValue(UInt32 ComHandle, byte _Sec, byte _Block, ref byte _BlockDataLen, byte[] _BlockData);

        [DllImport("CRT_570.dll")]
        public static extern int RF_Increment(UInt32 ComHandle, byte _Sec, byte _Block, byte _DataLen, byte[] _Data);

        [DllImport("CRT_570.dll")]
        public static extern int RF_Decrement(UInt32 ComHandle, byte _Sec, byte _Block, byte _DataLen, byte[] _Data);


    }
}
