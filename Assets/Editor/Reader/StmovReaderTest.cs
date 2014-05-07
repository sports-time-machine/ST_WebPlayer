using UnityEngine;
using System.IO;
using System.Collections;
using UnityTest;
using NUnit.Framework;
using SportsTimeMachinePlayer.Reader;
using SportsTimeMachinePlayer.Model;
using System.Collections.Generic;

namespace SportsTimeMachinePlayerTest.Reader{
	public class StmovReaderTest {	

		StmovReader reader;

		[SetUp]
		public void SetUp(){
			FileStream stream = new FileStream (Application.dataPath + "/Editor/TestData.stmov",FileMode.Open, FileAccess.Read);
			reader = new StmovReader (stream);
		}
		/// <summary>
		/// Reads the signature test.
		/// </summary>
		[Test]
		public void ReadSignatureTest(){
			Assert.AreEqual ("STMV  ", reader.ReadSignature());
		}

		/// <summary>
		/// Reads the version test.
		/// </summary>
		[Test]
		public void ReadVersionTest(){
			Assert.AreEqual ("1.1", reader.ReadVersion ()); 
		}

		/// <summary>
		/// Reads the total frames test.
		/// </summary>
		[Test]
		public void ReadTotalFramesTest(){
			Assert.AreEqual (1270, reader.ReadTotalFrames ());
		}

		/// <summary>
		/// Reads the total milli seconds test.
		/// </summary>
		[Test]
		public void ReadTotalMilliSecondsTest(){
			Assert.AreEqual (42333, reader.ReadTotalMilliSeconds ());
		}
		/// <summary>
		/// Reads the compress format test.
		/// </summary>
		[Test]
		public void ReadCompressFormatTest(){
			Assert.AreEqual ("depth 2d 10b/6b", reader.ReadCompressFormat().GetFormatName());
		}

		/// <summary>
		/// Reads the camera1 info test.
		/// </summary>
		[Test]
		public void ReadCamera1InfoTest(){
			CameraInfo info = reader.ReadCamera1Info ();
			Vector3 pos = info.Position;
			Vector3 rot = info.Rotation;
			Vector3 scale = info.Scale;

			Assert.AreEqual (-0.802999973f, pos.x);
			Assert.AreEqual (2.81599998f, pos.y);
			Assert.AreEqual (-1.82299995f, pos.z);

			Assert.AreEqual (0.674000025f, rot.x);
			Assert.AreEqual (0.0419999994f, rot.y);
			Assert.AreEqual (-0.0759999976f, rot.z);

			Assert.AreEqual (1.02999997f, scale.x);
			Assert.AreEqual (1.02999997f, scale.y);
			Assert.AreEqual (1.02999997f, scale.z);
		}

		/// <summary>
		/// Reads the camera2 info test.
		/// </summary>
		[Test]
		public void ReadCamera2InfoTest(){
			CameraInfo info = reader.ReadCamera2Info ();
			Vector3 pos = info.Position;
			Vector3 rot = info.Rotation;
			Vector3 scale = info.Scale;
			
			Assert.AreEqual (1.03999996f, pos.x);
			Assert.AreEqual (2.71300006f, pos.y);
			Assert.AreEqual (-2.0f, pos.z);
			
			Assert.AreEqual (0.560000002f, rot.x);
			Assert.AreEqual (0.0860000029f, rot.y);
			Assert.AreEqual (-0.0759999976f, rot.z);
			
			Assert.AreEqual (1.11000001f, scale.x);
			Assert.AreEqual (1.11000001f, scale.y);
			Assert.AreEqual (1.11000001f, scale.z);
		}

		/// <summary>
		/// フレームのリスト情報を取得できること.
		/// </summary>
		[Test]
		public void ReadFrameListTest(){
			List<Frame> frames = reader.ReadFrames ();

			Assert.AreEqual (1270, frames.Count);
		}
	}
}
