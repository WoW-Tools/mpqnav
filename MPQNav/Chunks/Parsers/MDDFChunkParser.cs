﻿using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using MPQNav.Util;

namespace MPQNav.Chunks.Parsers {
	internal class MDDFChunkParser : ChunkParser<MDDF[]> {
		private readonly string[] _names;

		public MDDFChunkParser(BinaryReader br, long pAbsoluteStart, string[] names)
			: base("MDDF", br, pAbsoluteStart) {
			_names = names;
		}

		/// <summary>
		/// Parse MDDF element from file stream
		/// </summary>
		public override MDDF[] Parse() {
			var _MDDF = new List<MDDF>();
			Reader.BaseStream.Position = AbsoluteStart;
			int bytesRead = 0;
			while(bytesRead < Size) {
				var lMDDF = new MDDF {
				                     	FileName = _names[(int)Reader.ReadUInt32()],
				                     	UniqId = Reader.ReadUInt32(),
				                     	Position = new Vector3(Reader.ReadSingle(),
				                     	                       Reader.ReadSingle(),
				                     	                       Reader.ReadSingle()),
				                     	Rotation = new Vector3(Reader.ReadSingle(),
				                     	                       Reader.ReadSingle(),
				                     	                       Reader.ReadSingle()),
				                     	Scale = (Reader.ReadUInt32() / 1024f)
				                     };
				bytesRead += 36; // 36 total bytes
				_MDDF.Add(lMDDF);
			}
			return _MDDF.ToArray();
		}
	}
}