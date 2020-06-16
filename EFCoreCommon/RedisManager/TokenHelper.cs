using EFCoreCommon;
using EFCoreCommon.Token;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebCore_Entity.RedisManager
{
	public static class TokenHelper
	{
		/// <summary>
		/// CacheKey
		/// </summary>
		public static readonly string CacheKey = "_Token";

		/// <summary>
		/// 有效时间
		/// </summary>
		public static readonly TimeSpan Expiry = new TimeSpan(0, 30, 0);

		/// <summary>
		/// 设置
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="prefix"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static TokenModel<T> Set<T>(string prefix, T data) where T : TokenData
		{
			var sign = string.Format("{0}:{1}", prefix, data.Username);
			var token = new TokenModel<T>()
			{
				UserId = data.Id,
				Signature = MD5Helper.Encrypt(sign),
				Expiry = DateTime.UtcNow.Add(Expiry),
				Data = data
			};
			RedisHelper.Database.HashSet(CacheKey, token.Signature, token);
			return token;
		}

		/// <summary>
		/// 设置
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="data"></param>
		/// <param name="suffix"></param>
		/// <returns></returns>
		public static TokenModel<T> Set<T>(T data, string suffix = "") where T : TokenData
		{
			var key = string.Format("{0}-{1}", CacheKey, suffix);
			var sign = string.Format("{0}", data.Username);
			var token = new TokenModel<T>()
			{
				UserId = data.Id,
				Signature = MD5Helper.Encrypt(sign),
				Expiry = DateTime.UtcNow.Add(Expiry),
				Data = data
			};
			RedisHelper.Database.HashSet(key, token.Signature, token);
			return token;
		}

		/// <summary>
		/// 获取
		/// </summary>
		/// <param name="signature">签名</param>
		/// <param name="suffix"></param>
		/// <returns></returns>
		public static TokenModel<T> Get<T>(string signature, string suffix = "") where T : TokenData
		{
			var key = string.Format("{0}-{1}", CacheKey, suffix);
			return RedisHelper.Database.HashGet<TokenModel<T>>(key, signature);
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="signature">签名</param>
		/// <param name="suffix"></param>
		/// <returns></returns>
		public static TokenModel<T> Verify<T>(string signature, string suffix = "") where T : TokenData
		{
			var redis = RedisHelper.Database;
			var key = string.Format("{0}-{1}", CacheKey, suffix);
			var token = redis.HashGet<TokenModel<T>>(key, signature);
			if (token == null || token.Expiry < DateTime.UtcNow)
			{
				return null;
			}
			//续时
			token.Expiry = DateTime.UtcNow.Add(Expiry);
			redis.HashSet(key, token.Signature, token);
			return token;
		}
	}
}
