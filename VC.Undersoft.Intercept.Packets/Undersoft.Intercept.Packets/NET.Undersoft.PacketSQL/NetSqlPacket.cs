#pragma once
#include "stdafx.h"

//using System;
//using System.Windows.Forms;
//



//WYWO£ANIE  PacketSQL.Parser( ref mNode, PacketData, ref Index, ref LItem, true );



//namespace MyClasses
//{
//
//	public class PacketSQL
//	{
//
//		public struct PACKET_SQL
//		{
//			public byte type;
//			public byte responseType;
//			public string sql;
//		}
//
//
//		public PacketSQL()
//		{
//
//		}
//
//		static string DecodeType(int i)
//		{
//			string dt = "Unknown";
//
//			switch (i)
//			{
//			case 239:
//				dt = "nchar";
//				break;
//			case 38:
//				dt = "int (check length for type of int)";
//				break;
//			case 48:
//				dt = "tinyint";
//				break;
//			case 56:
//				dt = "int";
//				break;
//			case 34:
//				dt = "image";
//				break;
//			case 35:
//				dt = "text";
//				break;
//			case 108:
//				dt = "numeric";
//				break;
//			case 52:
//				dt = "smallint";
//				break;
//			case 127:
//				dt = "bigint";
//				break;
//			case 111:
//				dt = "datetime (check length for type of datetime)";
//				break;
//			case 58:
//				dt = "smalldatetime";
//				break;
//			case 61:
//				dt = "datetime";
//				break;
//			case 167:
//				dt = "varchar";
//				break;
//			case 175:
//				dt = "char";
//				break;
//			case 104:
//			case 50:
//				dt = "bit";
//				break;
//			case 59:
//				dt = "real";
//				break;
//			case 62:
//				dt = "float";
//				break;
//			case 109:
//				dt = "float (check length for type of float)";
//				break;
//			case 165:
//				dt = "varbinary";
//				break;
//			case 122:
//				dt = "smallmoney";
//				break;
//			case 98:
//				dt = "sqlvariant";
//				break;
//			case 106:
//				dt = "decimal";
//				break;
//			case 36:
//				dt = "uniqueidentifier";
//				break;
//			case 60:
//				dt = "money";
//				break;
//			case 99:
//				dt = "ntext";
//				break;
//			case 173:
//				dt = "binary/timestamp";
//				break;
//			case 110:
//				dt = "money (check length for type of money)";
//				break;
//			case 231:
//				dt = "nvarchar";
//				break;
//			default:
//				dt = "Unknown : " + i.ToString();
//				break;
//			}
//
//			return dt;
//		}
//
//		static void ProcessCol(ref TreeNode mNodey, byte[] PacketData, ref int Index)
//		{
//			Dump2ByteInt("Usertype id : ", mNodey, PacketData, ref Index);
//
//			int flags = Function.Get2Bytes(PacketData, ref Index, Const.VALUE);
//			string fl = "Flags : ";
//			if ((flags & 0x01) != 0)
//			{
//				fl += " nullable,";
//			}
//			if ((flags & 0x02) != 0)
//			{
//				fl += " case sensitive,";
//			}
//			if ((flags & 0x04) != 0 || (flags & 0x08) != 0)
//			{
//				fl += " is writable,";
//			}
//			if ((flags & 0x10) != 0)
//			{
//				fl += " auto increment,";
//			}
//			mNodey.Nodes.Add(fl);
//			Function.SetPosition(ref mNodey, Index - 2, 2, false);
//
//			int datatype = PacketData[Index++];
//
//			string dt = DecodeType(datatype);
//			mNodey.Nodes.Add("Datatype : " + dt);
//			Function.SetPosition(ref mNodey, Index - 1, 1, false);
//
//			if (datatype == 35 || datatype == 99) // TEXT, NTEXT, IMAGE
//			{
//				Dump4ByteInt("Unknown : ", mNodey, PacketData, ref Index);
//				Dump1ByteInt("Unknown : ", mNodey, PacketData, ref Index);
//				Dump4ByteInt("Column size : ", mNodey, PacketData, ref Index);
//				int coltablen = Dump2ByteInt("Column table length : ", mNodey, PacketData, ref Index);
//				DumpString("Column table name : ", mNodey, PacketData, ref Index, coltablen);
//			}
//			else if (datatype == 98)
//			{
//				Dump4ByteInt("Unknown : ", mNodey, PacketData, ref Index);
//			}
//			else if (datatype == 34)
//			{
//				Dump4ByteInt("Column size : ", mNodey, PacketData, ref Index);
//				int coltablen = Dump2ByteInt("Column table length : ", mNodey, PacketData, ref Index);
//				DumpString("Column table name : ", mNodey, PacketData, ref Index, coltablen);
//			}
//			else if (datatype == 106 || datatype == 108) // NUMERIC, DECIMAL
//			{
//				Dump1ByteInt("Column size : ", mNodey, PacketData, ref Index);
//				Dump1ByteInt("Precision : ", mNodey, PacketData, ref Index);
//				Dump1ByteInt("Scale : ", mNodey, PacketData, ref Index);
//			}
//			else if (datatype == 111 || datatype == 38 || datatype == 109 ||
//				datatype == 110 || datatype == 104 || datatype == 36)
//			{
//				Dump1ByteInt("Column size : ", mNodey, PacketData, ref Index);
//			}
//			else if (datatype == 173 || datatype == 165)
//			{
//				Dump2ByteInt("Column size : ", mNodey, PacketData, ref Index);
//			}
//			else if ((datatype & 0x80) != 0)
//			{
//				Dump2ByteInt("Column size : ", mNodey, PacketData, ref Index);
//				Dump4ByteInt("Unknown : ", mNodey, PacketData, ref Index);
//				Dump1ByteInt("Unknown : ", mNodey, PacketData, ref Index);
//			}
//			else
//			{
//			}
//
//			int colnamelen = Dump1ByteInt("Column name length : ", mNodey, PacketData, ref Index);
//			DumpString("Column name : ", mNodey, PacketData, ref Index, colnamelen);
//		}
//
//		static int ProcessTable(ref TreeNode mNodex, byte[] PacketData, ref int Index)
//		{
//			int tabnamelen = Dump2ByteInt("Table name length : ", mNodex, PacketData, ref Index);
//			DumpString("Table name : ", mNodex, PacketData, ref Index, tabnamelen);
//
//			return 2 + tabnamelen * 2 + 1;
//		}
//
//		static int Dump1ByteInt(string desc, TreeNode mNodex, byte[] PacketData, ref int Index)
//		{
//			int i = PacketData[Index++];
//			mNodex.Nodes.Add(desc + i.ToString());
//			Function.SetPosition(ref mNodex, Index - 1, 1, false);
//
//			return i;
//		}
//
//		static int Dump2ByteInt(string desc, TreeNode mNodex, byte[] PacketData, ref int Index)
//		{
//			int i = Function.Get2Bytes(PacketData, ref Index, Const.VALUE);
//			mNodex.Nodes.Add(desc + i.ToString());
//			Function.SetPosition(ref mNodex, Index - 2, 2, false);
//
//			return i;
//		}
//
//		static ulong Dump4ByteInt(string desc, TreeNode mNodex, byte[] PacketData, ref int Index)
//		{
//			ulong i = Function.Get4Bytes(PacketData, ref Index, Const.VALUE);
//			mNodex.Nodes.Add(desc + i.ToString());
//			Function.SetPosition(ref mNodex, Index - 4, 4, false);
//
//			return i;
//		}
//
//		static string DumpString(string desc, TreeNode mNodex, byte[] PacketData, ref int Index, int msglen)
//		{
//			string s = Function.GetString(PacketData, ref Index, true, msglen * 2);
//			mNodex.Nodes.Add(desc + s);
//			Function.SetPosition(ref mNodex, Index - msglen * 2, msglen * 2, false);
//
//			return s;
//		}
//
//		static bool Process4(ref TreeNode mNodex, byte[] PacketData, ref int Index)
//		{
//			int responseType = PacketData[Index++];
//			string responsetype = string.Empty;
//			switch (responseType)
//			{
//			case 0x21:
//				responsetype = "language packet";
//				break;
//			case 0x71:
//				responsetype = "Logout";
//				break;
//			case 0x79:
//				responsetype = "TDS_RETURNSTATUS";
//				break;
//			case 0x7C:
//				responsetype = "process id";
//				break;
//			case 0x81:
//				responsetype = "TDS_COLMETADATA";
//				break;
//			case 0x88:
//				responsetype = "TDS_ALTCOLMETADATA";
//				break;
//			case 0xA0:
//				responsetype = "Column name";
//				break;
//			case 0xA1:
//				responsetype = "Column info";
//				break;
//			case 0xA4:
//				responsetype = "TDS_TABNAME";
//				break;
//			case 0xA5:
//				responsetype = "Column info 2";
//				break;
//			case 0xA7:
//				responsetype = "Compute related";
//				break;
//			case 0xA8:
//				responsetype = "Colimn Info - Compute result";
//				break;
//			case 0xA9:
//				responsetype = "TDS_ORDER";
//				break;
//			case 0xAA:
//				responsetype = "TDS_ERROR";
//				break;
//			case 0xAB:
//				responsetype = "TDS_INFO";
//				break;
//			case 0xAC:
//				responsetype = "TDS_PARAM";
//				break;
//			case 0xAD:
//				responsetype = "TDS_LOGINACK";
//				break;
//			case 0xAE:
//				responsetype = "Control";
//				break;
//			case 0xD1:
//				responsetype = "TDS_ROW";
//				break;
//			case 0xD3:
//				responsetype = "TDS_ALTROW";
//				break;
//			case 0xD7:
//				responsetype = "Param packet";
//				break;
//			case 0xE2:
//				responsetype = "Capability packet";
//				break;
//			case 0xE3:
//				responsetype = "TDS_ENVCHANGE";
//				break;
//			case 0xE5:
//				responsetype = "Extended error message";
//				break;
//			case 0xE6:
//				responsetype = "DBRPC";
//				break;
//			case 0xEC:
//				responsetype = "Param format packet";
//				break;
//			case 0xEE:
//				responsetype = "Result set";
//				break;
//			case 0xFD:
//				responsetype = "TDS_DONE";
//				break;
//			case 0xFE:
//				responsetype = "TDS_DONEPROC";
//				break;
//			case 0xFF:
//				responsetype = "TDS_DONEINPROC";
//				break;
//			default:
//				responsetype = "Unkown " + responseType.ToString();
//				break;
//			}
//			mNodex.Nodes.Add("Response Type : " + responsetype);
//			Function.SetPosition(ref mNodex, Index - 1, 1, false);
//
//			if (responseType == 0xFD || responseType == 0xFE || responseType == 0xFF)
//			{
//				int flags = Function.Get2Bytes(PacketData, ref Index, Const.VALUE);
//				string fl = "Flags : ";
//
//				if ((flags & 0x01) != 0)
//				{
//					fl += " More Results,";
//				}
//				if ((flags & 0x02) != 0)
//				{
//					fl += " Error,";
//				}
//				if ((flags & 0x10) != 0)
//				{
//					fl += " SELECT/INSERT/UPDATE(?),";
//				}
//				if ((flags & 0x20) != 0)
//				{
//					fl += " Cancel ack,";
//				}
//				mNodex.Nodes.Add(fl);
//				Function.SetPosition(ref mNodex, Index - 2, 2, false);
//
//				int operation = Function.Get2Bytes(PacketData, ref Index, Const.VALUE);
//				string op = string.Empty;
//				switch (operation)
//				{
//				case 0xB9:
//					op = "IMPLICIT TRANSACTION ON";
//					break;
//				case 0xBA:
//					op = "IMPLICIT TRANSACTION OFF";
//					break;
//				case 0xC0:
//					op = "IF (block start)(?)";
//					break;
//				case 0xC1:
//					op = "SELECT";
//					break;
//				case 0xC3:
//					op = "INSERT";
//					break;
//				case 0xC5:
//					op = "UPDATE";
//					break;
//				case 0xC6:
//					op = "CREATE TABLE";
//					break;
//				case 0xC7:
//					op = "DROP TABLE FAILED";
//					break;
//				case 0xCA:
//					op = "ENDIF (block end)(?)";
//					break;
//				case 0xD2:
//					op = "ROLLBACK";
//					break;
//				case 0xD5:
//					op = "COMMIT";
//					break;
//				case 0xDE:
//					op = "CREATE PROCEDURE";
//					break;
//				case 0xDF:
//					op = "DROP PROCEDURE";
//					break;
//				case 0xE0:
//					op = "END PROCEDURE";
//					break;
//				case 0xE2:
//					op = "USE (database change)";
//					break;
//				case 0xF9:
//					op = "SET";
//					break;
//				case 0xFD:
//					op = "Syntac error or batch execution interupted (?)";
//					break;
//				default:
//					op = "Unkown " + operation.ToString();
//					break;
//				}
//				mNodex.Nodes.Add("Operation : " + op);
//				Function.SetPosition(ref mNodex, Index - 2, 2, false);
//
//				Dump4ByteInt("Affected rows : ", mNodex, PacketData, ref Index);
//			}
//			else if (responseType == 0xD1)
//			{
//				mNodex.Nodes.Add("Parsing abandoned as TDS_ROW data is too hard");
//				return false;
//			}
//			else if (responseType == 0x81)
//			{
//				int cols = Dump2ByteInt("Columns : ", mNodex, PacketData, ref Index);
//
//				for (int i = 0; i < cols; i++)
//				{
//					int oldIndex = Index;
//					TreeNode mNodey = new TreeNode();
//					mNodey.Text = "SQL ( COL" + i.ToString() + " )";
//					ProcessCol(ref mNodey, PacketData, ref Index);
//					mNodex.Nodes.Add(mNodey);
//					Function.SetPosition(ref mNodex, oldIndex, Index - oldIndex, false);
//				}
//			}
//			else if (responseType == 0x79)
//			{
//				Dump4ByteInt("Return status : ", mNodex, PacketData, ref Index);
//			}
//			else if (responseType == 0xA5)
//			{
//				int packetsize = Dump2ByteInt("PacketSize : ", mNodex, PacketData, ref Index);
//				int i = 0;
//				int b = 1;
//				while (i < packetsize)
//				{
//					int lookahead = PacketData[Index];
//					if (lookahead != b)
//					{
//						int colnamelen = Dump1ByteInt("Col name length(?) : ", mNodex, PacketData, ref Index);
//						DumpString("Col name : ", mNodex, PacketData, ref Index, colnamelen);
//						i += 2;
//						i += 2 * colnamelen;
//					}
//					if (i < packetsize)
//					{
//						int x = Dump1ByteInt("Col(?) : ", mNodex, PacketData, ref Index);
//						Dump2ByteInt("(?) : ", mNodex, PacketData, ref Index);
//						i += 3;
//						b++;
//					}
//				}
//			}
//			else if (responseType == 0xA4)
//			{
//				int packetsize = Dump2ByteInt("PacketSize : ", mNodex, PacketData, ref Index);
//				Dump1ByteInt("Unknown : ", mNodex, PacketData, ref Index);
//
//				int i = 0;
//				while (i < packetsize)
//				{
//					i += ProcessTable(ref mNodex, PacketData, ref Index);
//					if (i < packetsize)
//					{
//						Dump1ByteInt("Unknown : ", mNodex, PacketData, ref Index);
//					}
//				}
//			}
//			else if (responseType == 0xA9)
//			{
//				int packetsize = Dump2ByteInt("PacketSize : ", mNodex, PacketData, ref Index);
//				for (int i = 0; i < packetsize; i += 2)
//				{
//					Dump2ByteInt("Order col : ", mNodex, PacketData, ref Index);
//				}
//			}
//			else if (responseType == 0xAD)
//			{
//				int packetsize = Dump2ByteInt("PacketSize : ", mNodex, PacketData, ref Index);
//				Dump1ByteInt("Unknown : ", mNodex, PacketData, ref Index);
//				Dump4ByteInt("TDS Version : ", mNodex, PacketData, ref Index);
//				int prodnamelen = Dump1ByteInt("Database product name length : ", mNodex, PacketData, ref Index);
//				DumpString("Database product name : ", mNodex, PacketData, ref Index, prodnamelen);
//				Dump4ByteInt("Database Version : ", mNodex, PacketData, ref Index);
//			}
//			else if (responseType == 0xAC)
//			{
//				int packetsize = Dump2ByteInt("PacketSize : ", mNodex, PacketData, ref Index);
//				int paramnamelen = Dump1ByteInt("Param name length : ", mNodex, PacketData, ref Index);
//				DumpString("Parameter name : ", mNodex, PacketData, ref Index, paramnamelen);
//				Dump1ByteInt("Unknown : ", mNodex, PacketData, ref Index);
//				int type = PacketData[Index];
//				Dump1ByteInt("Formal type(?) : " + DecodeType(type) + " : ", mNodex, PacketData, ref Index);
//				Dump2ByteInt("Unknown : ", mNodex, PacketData, ref Index);
//				Dump1ByteInt("Unknown : ", mNodex, PacketData, ref Index);
//				type = PacketData[Index];
//				Dump1ByteInt("Actual type : " + DecodeType(type) + " : ", mNodex, PacketData, ref Index);
//				mNodex.Nodes.Add("Parsing abandoned as TDS_PARAM is too hard");
//				for (int i = 1 + paramnamelen + 1 + 1 + 2 + 1 + 1; i < packetsize; i++)
//				{
//					Dump1ByteInt("Unknown : ", mNodex, PacketData, ref Index);
//				}
//			}
//			else if (responseType == 0xE3)
//			{
//				int packetsize = Dump2ByteInt("PacketSize : ", mNodex, PacketData, ref Index);
//
//				int changetype = Dump1ByteInt("Change type : ", mNodex, PacketData, ref Index);
//
//				switch (changetype)
//				{
//				case 1:
//					int dbnamelen = Dump1ByteInt("Database name len : ", mNodex, PacketData, ref Index);
//					DumpString("Database name : ", mNodex, PacketData, ref Index, dbnamelen);
//					dbnamelen = Dump1ByteInt("Old database name len : ", mNodex, PacketData, ref Index);
//					DumpString("Old database name : ", mNodex, PacketData, ref Index, dbnamelen);
//					break;
//				case 3:
//					int charsetlen = Dump1ByteInt("Charset len : ", mNodex, PacketData, ref Index);
//					DumpString("Charset : ", mNodex, PacketData, ref Index, charsetlen);
//					Dump2ByteInt("Charset : ", mNodex, PacketData, ref Index);
//					charsetlen = Dump1ByteInt("Old charset len : ", mNodex, PacketData, ref Index);
//					DumpString("Old charset : ", mNodex, PacketData, ref Index, charsetlen);
//					break;
//				case 4:
//					int blocksizelen = Dump1ByteInt("Block size len : ", mNodex, PacketData, ref Index);
//					DumpString("Blocksize : ", mNodex, PacketData, ref Index, blocksizelen);
//					blocksizelen = Dump1ByteInt("Old blocksize len : ", mNodex, PacketData, ref Index);
//					DumpString("Old block size : ", mNodex, PacketData, ref Index, blocksizelen);
//					break;
//				default:
//					mNodex.Nodes.Add("Unrecognised ENVCHANGE parsing abandoned ");
//					for (int i = 1; i < packetsize; i++)
//					{
//						Dump1ByteInt("Unknown : ", mNodex, PacketData, ref Index);
//					}
//					break;
//				}
//			}
//			else if (responseType == 0xAA || responseType == 0xAB)
//			{
//				int packetsize = Dump2ByteInt("PacketSize : ", mNodex, PacketData, ref Index);
//				Dump4ByteInt("Error message number : ", mNodex, PacketData, ref Index);
//				Dump1ByteInt("State : ", mNodex, PacketData, ref Index);
//				Dump1ByteInt("Severity : ", mNodex, PacketData, ref Index);
//				int msglen = Dump2ByteInt("Message length : ", mNodex, PacketData, ref Index);
//				DumpString("Message : ", mNodex, PacketData, ref Index, msglen);
//				int svrlen = Dump1ByteInt("Server name length : ", mNodex, PacketData, ref Index);
//				DumpString("Server name : ", mNodex, PacketData, ref Index, svrlen);
//				int proclen = Dump1ByteInt("Procedure name length : ", mNodex, PacketData, ref Index);
//				DumpString("Procedure name : ", mNodex, PacketData, ref Index, proclen);
//				Dump2ByteInt("Line num : ", mNodex, PacketData, ref Index);
//			}
//			else
//			{
//				mNodex.Nodes.Add("Unrecognised TDS_* parsing abandoned");
//				return false;
//
//			}
//
//			return true;
//		}
//
//		public static bool Parser(ref TreeNodeCollection mNode,
//			byte[] PacketData,
//			ref int Index,
//			ref ListViewItem LItem, bool DisplayData)
//		{
//			string Tmp = "";
//			TreeNode mNodex;
//			int Size = 0;
//			PACKET_SQL PSql;
//
//			Size = PacketData.GetLength(0) - Index;
//
//			mNodex = new TreeNode();
//			mNodex.Text = "SQL ( SQL )";
//			Function.SetPosition(ref mNodex, Index, PacketData.Length - Index, true);
//
//			if (Index >= PacketData.GetLength(0))
//			{
//				mNodex.Nodes.Add("No data.");
//				return false;
//			}
//
//			if (PacketData.GetLength(0) == 1514)
//			{
//				mNodex.Nodes.Add("Packet is very likely fragmented so parsing is unlikely to work.");
//			}
//
//			try
//			{
//				PSql.type = PacketData[Index++];
//				string type = string.Empty;
//				switch (PSql.type)
//				{
//				case 1:
//					type = "Query from client";
//					break;
//				case 2:
//					type = "Login from client";
//					break;
//				case 3:
//					type = "Stored procedure call from client";
//					break;
//				case 4:
//					type = "Reply from server";
//					break;
//				case 6:
//					type = "Cancel query from client";
//					break;
//				case 7:
//					type = "Bulk";
//					break;
//				case 15:
//					type = "Query from client 2";
//					break;
//				case 16:
//					type = "Login from client 2";
//					break;
//				default:
//					type = "Unkown " + PSql.type.ToString();
//					break;
//				}
//				mNodex.Nodes.Add("Type : " + type);
//				Function.SetPosition(ref mNodex, Index - 1, 1, false);
//
//				int lastpack = PacketData[Index++];
//				if (lastpack == 1)
//				{
//					mNodex.Nodes.Add("Last packet");
//					Function.SetPosition(ref mNodex, Index - 1, 1, false);
//				}
//				else
//				{
//					mNodex.Nodes.Add("More packets to come");
//					Function.SetPosition(ref mNodex, Index - 1, 1, false);
//				}
//
//				Dump2ByteInt("Packet Size : ", mNodex, PacketData, ref Index);
//				Dump4ByteInt("Unknown : ", mNodex, PacketData, ref Index);
//
//				if (PSql.type == 1)
//				{
//					int oldindex = Index;
//					PSql.sql = Function.GetString(PacketData, ref Index, true, Size - 8);
//					mNodex.Nodes.Add("SQL : " + PSql.sql);
//					Function.SetPosition(ref mNodex, oldindex, Index - oldindex, false);
//				}
//				else if (PSql.type == 2)
//				{
//				}
//				else if (PSql.type == 3)
//				{
//					int procnamelen = Dump2ByteInt("Proc name length : ", mNodex, PacketData, ref Index);
//					DumpString("Proc name : ", mNodex, PacketData, ref Index, procnamelen);
//				}
//				else if (PSql.type == 4)
//				{
//					bool cont = true;
//					while (cont && Index < PacketData.GetLength(0))
//					{
//						int oldIndex = Index;
//						TreeNode mNodey = new TreeNode();
//						mNodey.Text = "SQL ( SERVER RESPONSE )";
//
//						cont = Process4(ref mNodey, PacketData, ref Index);
//
//						mNodex.Nodes.Add(mNodey);
//						Function.SetPosition(ref mNodex, oldIndex, Index - oldIndex, false);
//					}
//				}
//				//					for( i = 0; i < PSql.Contents.GetLength(0); i ++ )
//				//					{
//				//						Tmp = (string) PSql.Contents.GetValue( i );
//				//						Tmp = Tmp.Trim();
//				//						if( Tmp != "" )
//				//							mNodex.Nodes.Add( Tmp + "\\r\\n" );
//				//					}
//
//				LItem.SubItems[Const.LIST_VIEW_PROTOCOL_INDEX].Text = "SQL";
//				LItem.SubItems[Const.LIST_VIEW_INFO_INDEX].Text = "TDS Protocol";
//
//				mNode.Add(mNodex);
//
//			}
//			catch (Exception Ex)
//			{
//				mNode.Add(mNodex);
//				Tmp = "[ Malformed SQL packet. Remaining bytes don't fit an SQL packet. Possibly due to bad decoding ]";
//				mNode.Add(Tmp);
//				Tmp = "[ Exception raised is <" + Ex.GetType().ToString() + "> at packet index <" + Index.ToString() + "> ]";
//				mNode.Add(Tmp);
//				LItem.SubItems[Const.LIST_VIEW_INFO_INDEX].Text = "[ Malformed SQL packet. Remaining bytes don't fit an SQL packet. Possibly due to bad decoding ]";
//
//				return false;
//			}
//
//			return true;
//
//		}
//
//
//		public static bool Parser(byte[] PacketData,
//			ref int Index,
//			ref ListViewItem LItem, bool DisplayData)
//		{
//			int Size = 0;
//			PACKET_SQL PSql;
//
//			try
//			{
//				Size = PacketData.GetLength(0) - Index;
//
//				PSql.type = PacketData[Index++];
//				//            PSql.length = Function.Get2Bytes(PacketData, ref Index, Const.VALUE);
//				//            PSql.number = Function.Get2Bytes(PacketData, ref Index, Const.VALUE);
//				//            PSql.state = PacketData[Index ++];
//				//            PSql.level = PacketData[Index ++];
//				//            PSql.textLength = Function.Get2Bytes(PacketData, ref Index, Const.VALUE);
//				//            PSql.text = Function.GetString(PacketData, ref Index, PSql.textLength);
//				//            PSql.nameLength = PacketData[Index ++];
//				//            PSql.name = Function.GetString(PacketData, ref Index, PSql.nameLength);
//				//            PSql.procNameLength = PacketData[Index ++];
//				//            PSql.procName = Function.GetString(PacketData, ref Index, PSql.procNameLength);
//				//            PSql.lineNumber = PacketData[Index ++];
//
//				LItem.SubItems[Const.LIST_VIEW_PROTOCOL_INDEX].Text = "SQL";
//				LItem.SubItems[Const.LIST_VIEW_INFO_INDEX].Text = "TDS Protocol";
//
//			}
//			catch
//			{
//				LItem.SubItems[Const.LIST_VIEW_INFO_INDEX].Text = "[ Malformed SQL packet. Remaining bytes don't fit an SQL packet. Possibly due to bad decoding ]";
//
//				return false;
//			}
//
//			return true;
//
//		}
//
//	}
//}