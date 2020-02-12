﻿//-----------------------------------------------------------------------
// <copyright file="CaptureCall.cs" company="nodus">
//     nodus.  All rights reserved.
// </copyright>
// <author>martin</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Turnkey.code;
using Turnkey.config;
using Turnkey.Parameter;

namespace Turnkey
{
    /// <summary>
    /// Do capture action
    /// </summary>
    public class CaptureCall : ApiCall
    {
        public CaptureCall(ApplicationConfig config, Dictionary<String, String> inputParams) : base(config, inputParams)
        {

        }

        protected override void PreValidateParams(Dictionary<String, String> inputParams)
        {
            List<String> requiredParams = new List<String>();
            requiredParams.Add("amount");
            requiredParams.Add("originalMerchantTxId");

            foreach (KeyValuePair<String, String> entry in inputParams)
            {
                if (entry.Value != null && entry.Value.Trim().Length > 0)
                {
                    requiredParams.Remove(entry.Key);
                }
            }

            InputParamAfterValidation(requiredParams);
        }

        protected override Dictionary<String, String> GetTokenParams(Dictionary<String, String> inputParams)
        {
            Dictionary<String, String> tokenParam = TokenParameters;
            tokenParam.Add("action",ActionType.CAPTURE.GetCode());
            GenerateRestParameters(inputParams, tokenParam, new ParamCaptureSessionToken());
            return tokenParam;
        }

        protected override Dictionary<String, String> GetActionParams(Dictionary<String, String> inputParams, String token)
        {
            Dictionary<String, String> actionParams = new Dictionary<String, String>();
            actionParams.Add("merchantId", config.MerchantId);
            actionParams.Add("token", token);
            GenerateRestParameters(inputParams, actionParams, new ParamCaptureAction());

            return actionParams;
        }

    }
}