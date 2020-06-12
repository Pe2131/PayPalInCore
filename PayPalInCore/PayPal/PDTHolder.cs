﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace PayPalInCore.PayPal
{
    public class PDTHolder
    {
        public double GrossTotal { get; set; }
        public int InvoiceNumber { get; set; }
        public string PaymentStatus { get; set; }
        public double PaymentFee { get; set; }
        public string BusinessEmail { get; set; }
        public string PayerEmail { get; set; }
        public string TxToken { get; set; }
        public string PayerLastName { get; set; }
        public string ReciverEmail { get; set; }
        public string ItemName { get; set; }
        public string Currency { get; set; }
        public string TransactionId { get; set; }
        public string SubscriberId { get; set; }
        public string Custom { get; set; }

        private static string IdentityToken, txToken, query, strResponse;
        public static PDTHolder Success(string tx)
        {
            PayPalConfig payPalConfig = PayPalService.GetPayPalConfig();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            IdentityToken = payPalConfig.IdentityToken;
            txToken = tx;
            query = string.Format("cmd=_notify-synch&tx={0}&at={1}", txToken, IdentityToken);
            string url = payPalConfig.PostUrl;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = query.Length;
            StreamWriter stOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
            stOut.Write(query);
            stOut.Close();
            StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
            strResponse = stIn.ReadToEnd();
            stIn.Close();
            if (strResponse.StartsWith("SUCCESS"))
                return PDTHolder.parse(strResponse);
            return null;

        }
        private static PDTHolder parse(string postData)
        {
            string sKey, sValue;
            PDTHolder ph = new PDTHolder();
            try
            {
                string[] StringArray = postData.Split('\n');
                int i;
                for (i = 0; i < StringArray.Length; i++)
                {
                    string[] StringArray1 = StringArray[i].Split('=');
                    sKey = StringArray1[0];
                    if (StringArray1.Length > 1)
                    {
                        sValue = HttpUtility.UrlDecode(StringArray1[1]);
                        switch (sKey)
                        {
                            case "mc_gross":
                                ph.GrossTotal = Convert.ToDouble(sValue);
                                break;

                            case "invoice":
                                ph.InvoiceNumber = Convert.ToInt32(sValue);
                                break;
                            case "payment_status":
                                ph.PaymentStatus = Convert.ToString(sValue);
                                break;
                            case "first_name":
                                ph.PaymentStatus = Convert.ToString(sValue);
                                break;
                            case "mc_fee":
                                ph.PaymentStatus = Convert.ToString(sValue);
                                break;
                            case "business":
                                ph.PaymentStatus = Convert.ToString(sValue);
                                break;
                            case "payer_email":
                                ph.PayerEmail = Convert.ToString(sValue);
                                break;
                            case "Tx_Token":
                                ph.TxToken = Convert.ToString(sValue);
                                break;
                            case "last_name":
                                ph.PayerLastName = Convert.ToString(sValue);
                                break;
                            case "receiver_email":
                                ph.ReciverEmail = Convert.ToString(sValue);
                                break;
                            case "item_name":
                                ph.ItemName = Convert.ToString(sValue);
                                break;
                            case "mc_currency":
                                ph.TxToken = Convert.ToString(sValue);
                                break;
                            case "txn_id":
                                ph.TransactionId = Convert.ToString(sValue);
                                break;
                            case "custom":
                                ph.Custom = Convert.ToString(sValue);
                                break;
                            case "subscr_id":
                                ph.SubscriberId = Convert.ToString(sValue);
                                break;
                        }
                    }
                }
                return ph;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
