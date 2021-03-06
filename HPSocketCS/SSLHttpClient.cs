﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace HPSocketCS
{
    public class HttpsClient : SSLHttpClient
    {

    }
    public class SSLHttpClient : HttpClient
    {

        static int ObjectReferer = 0;
        static string SSLInitLock = "SSL初始化锁";

        /// <summary>
        /// 验证模式
        /// </summary>
        public SSLVerifyMode VerifyMode { get; set; }
        /// <summary>
        /// 证书文件（客户端可选）
        /// </summary>
        public string PemCertFile { get; set; }
        /// <summary>
        /// 私钥文件（客户端可选）
        /// </summary>
        public string PemKeyFile { get; set; }
        /// <summary>
        /// 私钥密码（没有密码则为空）
        /// </summary>
        public string KeyPasswod { get; set; }
        /// <summary>
        /// CA 证书文件或目录（单向验证或客户端可选）
        /// </summary>
        public string CAPemCertFileOrPath { get; set; }

        public SSLHttpClient()
        {
            Interlocked.Increment(ref ObjectReferer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_verifyModel">验证模式</param>
        /// <param name="_pemCertFile">证书文件（客户端可选）</param>
        /// <param name="_pemKeyFile">私钥文件（客户端可选）</param>
        /// <param name="_keyPasswod">私钥密码（没有密码则为空）</param>
        /// <param name="_caPemCertFileOrPath">CA 证书文件或目录（单向验证或客户端可选）</param>
        public SSLHttpClient(SSLVerifyMode _verifyModel, string _pemCertFile, string _pemKeyFile, string _keyPasswod, string _caPemCertFileOrPath)
        {
            Interlocked.Increment(ref ObjectReferer);
            this.VerifyMode = _verifyModel;
            this.PemCertFile = _pemCertFile;
            this.PemKeyFile = _pemKeyFile;
            this.KeyPasswod = _keyPasswod;
            this.CAPemCertFileOrPath = _caPemCertFileOrPath;
            Initialize();
        }

        ~SSLHttpClient()
        {
            Uninitialize();
        }

        protected override bool CreateListener()
        {
            if (IsCreate == true || pListener != IntPtr.Zero || pClient != IntPtr.Zero)
            {
                return false;
            }

            pListener = HttpSdk.Create_HP_HttpClientListener();
            if (pListener == IntPtr.Zero)
            {
                return false;
            }

            pClient = SSLHttpSdk.Create_HP_HttpsClient(pListener);
            if (pClient == IntPtr.Zero)
            {
                return false;
            }

            IsCreate = true;

            return true;
        }

        /// <summary>
        /// 初始化SSL环境
        /// </summary>
        /// <returns></returns>
        protected virtual bool Initialize()
        {
            lock (SSLInitLock)
            {
                if (SSLSdk.HP_SSL_IsValid() == false)
                {

                    PemCertFile = string.IsNullOrWhiteSpace(PemCertFile) ? null : PemCertFile;
                    PemKeyFile = string.IsNullOrWhiteSpace(PemKeyFile) ? null : PemKeyFile;
                    KeyPasswod = string.IsNullOrWhiteSpace(KeyPasswod) ? null : KeyPasswod;
                    CAPemCertFileOrPath = string.IsNullOrWhiteSpace(CAPemCertFileOrPath) ? null : CAPemCertFileOrPath;


                    return SSLSdk.HP_SSL_Initialize(SSLSessionMode.Client, VerifyMode, PemCertFile, PemKeyFile, KeyPasswod, CAPemCertFileOrPath, null);

                }

                return true;
            }
        }

        /// <summary>
        /// 反初始化SSL环境
        /// </summary>
        protected virtual void Uninitialize()
        {
            if (Interlocked.Decrement(ref ObjectReferer) == 0)
            {
                SSLSdk.HP_SSL_Cleanup();
            }
        }

        public new bool Connect(string address, ushort port, bool async = false)
        {
            bool ret = false;
            if (Initialize())
            {
                ret = base.Connect(address, port, async);
            }

            return ret;
        }

        public override void Destroy()
        {
            Stop();
            if (pClient != IntPtr.Zero)
            {
                SSLHttpSdk.Destroy_HP_HttpsClient(pClient);
                pClient = IntPtr.Zero;
            }
            if (pListener != IntPtr.Zero)
            {
                HttpSdk.Destroy_HP_HttpClientListener(pListener);
                pListener = IntPtr.Zero;
            }

            IsCreate = false;
        }
    }
}
