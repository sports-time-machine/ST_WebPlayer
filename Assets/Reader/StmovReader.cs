using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using SportsTimeMachinePlayer.Model;
using System.Text;
using SportsTimeMachinePlayer.CompressFormat;

namespace SportsTimeMachinePlayer.Reader
{
	/// <summary>
	/// stmovデータからデータを読み取るクラス.
	/// </summary>
	public class StmovReader
	{
		private Stream stream;

		public StmovReader (Stream stream)
		{
			this.stream = stream;
		}

		/// <summary>
		/// シグネチャを読み込む.
		/// </summary>
		/// <returns>The signature.</returns>
		public String ReadSignature(){
			stream.Seek(0,SeekOrigin.Begin);
			byte[] bytes = new byte[6];
			stream.Read (bytes, 0, 6);
			String signature = Encoding.ASCII.GetString (bytes);
			return signature;
		}

		/// <summary>
		/// バージョンを読み込む.
		/// </summary>
		/// <returns>The version.</returns>
		public String ReadVersion(){
			stream.Seek (6, SeekOrigin.Begin);
			int majorVersion = stream.ReadByte();
			int minorVersion = stream.ReadByte();
			String version = majorVersion + "." + minorVersion; 
			return version;
		}

		/// <summary>
		/// 総フレーム数を読み込む.
		/// </summary>
		/// <returns>The total frames.</returns>
		public int ReadTotalFrames(){
			stream.Seek (8, SeekOrigin.Begin);
			byte[] bytes = new byte[sizeof(int)];
			stream.Read (bytes, 0, sizeof(int));
			int totalFrames = BitConverter.ToInt32(bytes, 0);
			return totalFrames;
		}

		/// <summary>
		/// 総ミリ秒を読み込む.
		/// </summary>
		/// <returns>The total milli seconds.</returns>
		public int ReadTotalMilliSeconds(){
			stream.Seek (12, SeekOrigin.Begin);
			byte[] bytes = new byte[sizeof(int)];
			stream.Read (bytes, 0, sizeof(int));
			int totalFrames = BitConverter.ToInt32(bytes, 0);
			return totalFrames;
		}

		/// <summary>
		/// 深度情報圧縮フォーマットを読み込む.
		/// </summary>
		/// <returns>The compress format.</returns>
		public ICompressFormat ReadCompressFormat(){
			stream.Seek (16, SeekOrigin.Begin);
			byte[] bytes = new byte[16];
			stream.Read (bytes, 0, 16);
			string formatString = Encoding.ASCII.GetString(bytes);
			ICompressFormat format = CompressFormat.FormatFactory.GetFormat(formatString);
			return format;
		}

		/// <summary>
		/// Reads the camera1 info.
		/// </summary>
		/// <returns>The camera1 info.</returns>
		public CameraInfo ReadCamera1Info(){
			stream.Seek (32, SeekOrigin.Begin);
			byte[] xBytes = new byte[sizeof(float)];
			byte[] yBytes = new byte[sizeof(float)];
			byte[] zBytes = new byte[sizeof(float)];

			// カメラ情報
			stream.Read (xBytes, 0, sizeof(float));
			stream.Read (yBytes, 0, sizeof(float));
			stream.Read (zBytes, 0, sizeof(float));
			Vector3 pos = new Vector3(
				BitConverter.ToSingle(xBytes, 0),
				BitConverter.ToSingle(yBytes, 0),
				BitConverter.ToSingle(zBytes, 0)
			);
			
			stream.Read (xBytes, 0, sizeof(float));
			stream.Read (yBytes, 0, sizeof(float));
			stream.Read (zBytes, 0, sizeof(float));
			Vector3 rot = new Vector3(
				BitConverter.ToSingle(xBytes, 0),
				BitConverter.ToSingle(yBytes, 0),
				BitConverter.ToSingle(zBytes, 0)
			);
			
			stream.Read (xBytes, 0, sizeof(float));
			stream.Read (yBytes, 0, sizeof(float));
			stream.Read (zBytes, 0, sizeof(float));
			Vector3 scale = new Vector3(
				BitConverter.ToSingle(xBytes, 0),
				BitConverter.ToSingle(yBytes, 0),
				BitConverter.ToSingle(zBytes, 0)
			);

			CameraInfo info = new CameraInfo (pos, rot, scale);
			return info;
		}

		/// <summary>
		/// Reads the camera2 info.
		/// </summary>
		/// <returns>The camera2 info.</returns>
		public CameraInfo ReadCamera2Info(){
			stream.Seek (68, SeekOrigin.Begin);
			byte[] xBytes = new byte[sizeof(float)];
			byte[] yBytes = new byte[sizeof(float)];
			byte[] zBytes = new byte[sizeof(float)];
			
			// カメラ情報
			stream.Read (xBytes, 0, sizeof(float));
			stream.Read (yBytes, 0, sizeof(float));
			stream.Read (zBytes, 0, sizeof(float));
			Vector3 pos = new Vector3(
				BitConverter.ToSingle(xBytes, 0),
				BitConverter.ToSingle(yBytes, 0),
				BitConverter.ToSingle(zBytes, 0)
				);
			
			stream.Read (xBytes, 0, sizeof(float));
			stream.Read (yBytes, 0, sizeof(float));
			stream.Read (zBytes, 0, sizeof(float));
			Vector3 rot = new Vector3(
				BitConverter.ToSingle(xBytes, 0),
				BitConverter.ToSingle(yBytes, 0),
				BitConverter.ToSingle(zBytes, 0)
				);
			
			stream.Read (xBytes, 0, sizeof(float));
			stream.Read (yBytes, 0, sizeof(float));
			stream.Read (zBytes, 0, sizeof(float));
			Vector3 scale = new Vector3(
				BitConverter.ToSingle(xBytes, 0),
				BitConverter.ToSingle(yBytes, 0),
				BitConverter.ToSingle(zBytes, 0)
				);
			
			CameraInfo info = new CameraInfo (pos, rot, scale);
			return info;
		}

		/// <summary>
		/// Reads the frames.
		/// </summary>
		/// <returns>The frames.</returns>
		public List<Frame> ReadFrames(){
			List<Frame> frames = new List<Frame>();

			int totalFrames = ReadTotalFrames();
			ICompressFormat format = ReadCompressFormat ();
			stream.Seek (108, SeekOrigin.Begin);

			for (int i=0; i < totalFrames ; i++) 
			{
				// ボクセル数.
				byte[] voxcelCountBuffer = new byte[sizeof(Int32)];
				stream.Read(voxcelCountBuffer, 0, sizeof(Int32));

				// フレームのサイズ
				byte[] voxcelSizeBuffer = new byte[sizeof(Int32)];
				stream.Read(voxcelSizeBuffer, 0, sizeof(Int32));
				Int32 voxcelSize = BitConverter.ToInt32(voxcelSizeBuffer, 0);


				// フレームデータ.
				byte[] voxcelDataBuffer = new byte[voxcelSize];
				stream.Read(voxcelDataBuffer, 0, voxcelSize);

				frames.Add(new Frame(voxcelDataBuffer, format));
			}

			return frames;
		}

	}
}

