#pragma once
#include "stdafx.h"

//using System;
//using System.Net;
//using System.Net.Sockets;
//
//public static void SetPosition(ref TreeNode mNode, int Start, int Len, bool BaseOrSub)
//{
//	if (BaseOrSub)
//		mNode.Tag = FindHighlightPos(Start, Len);
//	else
//		mNode.Nodes[mNode.Nodes.Count - 1].Tag = FindHighlightPos(Start, Len);
//
//}
//
//public static ushort Get2Bytes(byte[] ptr, ref int Index, int Type)
//{
//	ushort u = 0;
//
//	if (Type == NORMAL)
//	{
//		u = (ushort)ptr[Index++];
//		u *= 256;
//		u += (ushort)ptr[Index++];
//	}
//	else if (Type == VALUE)
//	{
//		u = (ushort)ptr[++Index];
//		u *= 256; Index--;
//		u += (ushort)ptr[Index++]; Index++;
//	}
//
//	return u;
//}
//
//public static void Set2Bytes(ref byte[] ptr, ref int Index, ushort NewValue, int Type)
//{
//
//	if (Type == NORMAL)
//	{
//		ptr[Index] = (byte)(NewValue >> 8);
//		ptr[Index + 1] = (byte)NewValue;
//	}
//	else if (Type == VALUE)
//	{
//		ptr[Index + 0] = (byte)NewValue;
//		ptr[Index + 1] = (byte)(NewValue >> 8);
//		Index += 2;
//	}
//
//}
//
//
//public static uint Get3Bytes(byte[] ptr, ref int Index, int Type)
//{
//	uint ui = 0;
//
//	if (Type == NORMAL)
//	{
//		ui = ((uint)ptr[Index++]) << 16;
//		ui += ((uint)ptr[Index++]) << 8;
//		ui += (uint)ptr[Index++];
//	}
//
//	return ui;
//}
//
//
//public static uint Get4Bytes(byte[] ptr, ref int Index, int Type)
//{
//	uint ui = 0;
//
//	if (Type == NORMAL)
//	{
//		ui = ((uint)ptr[Index++]) << 24;
//		ui += ((uint)ptr[Index++]) << 16;
//		ui += ((uint)ptr[Index++]) << 8;
//		ui += (uint)ptr[Index++];
//	}
//	else if (Type == VALUE)
//	{
//		ui = ((uint)ptr[Index + 3]) << 24;
//		ui += ((uint)ptr[Index + 2]) << 16;
//		ui += ((uint)ptr[Index + 1]) << 8;
//		ui += (uint)ptr[Index]; Index += 4;
//	}
//
//	return ui;
//}
//
//public static void Set4Bytes(ref byte[] ptr, int Index, uint NewValue, int Type)
//{
//
//	if (Type == NORMAL)
//	{
//		ptr[Index + 1] = (byte)(NewValue >> 24);
//		ptr[Index + 2] = (byte)(NewValue >> 16);
//		ptr[Index + 3] = (byte)(NewValue >> 8);
//		ptr[Index + 4] = (byte)NewValue;
//	}
//	else if (Type == VALUE)
//	{
//		ptr[Index + 0] = (byte)NewValue;
//		ptr[Index + 1] = (byte)(NewValue >> 8);
//		ptr[Index + 2] = (byte)(NewValue >> 16);
//		ptr[Index + 3] = (byte)(NewValue >> 24);
//	}
//
//}
//
//public static ulong Get8Bytes(byte[] ptr, ref int Index, int Type)
//{
//	ulong ui = 0;
//
//	if (Type == NORMAL)
//	{
//		ui = ((uint)ptr[Index++]) << 56;
//		ui += ((uint)ptr[Index++]) << 48;
//		ui += ((uint)ptr[Index++]) << 40;
//		ui += ((uint)ptr[Index++]) << 32;
//		ui += ((uint)ptr[Index++]) << 24;
//		ui += ((uint)ptr[Index++]) << 16;
//		ui += ((uint)ptr[Index++]) << 8;
//		ui += (uint)ptr[Index++];
//	}
//	else if (Type == VALUE)
//	{
//		ui = ((uint)ptr[Index + 7]) << 56;
//		ui += ((uint)ptr[Index + 6]) << 48;
//		ui += ((uint)ptr[Index + 5]) << 40;
//		ui += ((uint)ptr[Index + 4]) << 32;
//		ui += ((uint)ptr[Index + 3]) << 24;
//		ui += ((uint)ptr[Index + 2]) << 16;
//		ui += ((uint)ptr[Index + 1]) << 8;
//		ui += (uint)ptr[Index]; Index += 8;
//	}
//
//	return ui;
//}
//
//public static void Set8Bytes(ref byte[] ptr, int Index, ulong NewValue, int Type)
//{
//	if (Type == NORMAL)
//	{
//		ptr[Index] = (byte)(NewValue >> 56);
//		ptr[Index + 1] = (byte)(NewValue >> 48);
//		ptr[Index + 2] = (byte)(NewValue >> 40);
//		ptr[Index + 3] = (byte)(NewValue >> 32);
//		ptr[Index + 4] = (byte)(NewValue >> 24);
//		ptr[Index + 5] = (byte)(NewValue >> 16);
//		ptr[Index + 6] = (byte)(NewValue >> 8);
//		ptr[Index + 7] = (byte)NewValue;
//	}
//	else if (Type == VALUE)
//	{
//		ptr[Index + 7] = (byte)(NewValue >> 56);
//		ptr[Index + 6] = (byte)(NewValue >> 48);
//		ptr[Index + 5] = (byte)(NewValue >> 40);
//		ptr[Index + 4] = (byte)(NewValue >> 32);
//		ptr[Index + 3] = (byte)(NewValue >> 24);
//		ptr[Index + 2] = (byte)(NewValue >> 16);
//		ptr[Index + 1] = (byte)(NewValue >> 8);
//		ptr[Index] = (byte)NewValue;
//	}
//
//}
//
//public static string FindString(byte[] PacketData, ref int Index)
//{
//	string Tmp = "";
//
//	while (PacketData[Index] != 0)
//	{
//		Tmp += (char)PacketData[Index++];
//	}
//
//	Index++;
//
//	return Tmp;
//}
//
//
//public static string GetString(byte[] PacketData, ref int Index, string FormatText, int len)
//{
//	string Tmp = "";
//	int i = 0;
//
//	for (i = 0; i < len; i++)
//	{
//		Tmp += PacketData[Index++].ToString(FormatText);
//	}
//
//	return Tmp;
//}
//
//
//public static string GetString(byte[] PacketData, ref int Index, int len)
//{
//	string Tmp = "";
//	int i = 0;
//
//	for (i = 0; i < len; i++)
//	{
//		Tmp += (char)PacketData[Index++];
//	}
//
//	return Tmp;
//}
//
//public static string GetString(byte[] PacketData, ref int Index)
//{
//	string Tmp = "";
//
//	while (PacketData[Index] != 0)
//	{
//		Tmp += (char)PacketData[Index++];
//	}
//
//	Index++;
//
//	return Tmp;
//}
//
//// <KEITH>
//
//public static string GetString(byte[] PacketData, ref int Index, bool IsUnicode, int len)
//{
//	string Tmp = "";
//	int used = 0;
//
//	if (IsUnicode)
//	{
//		while ((used < len && PacketData[Index] != 0) && (PacketData[Index + 1] == 0))
//		{
//			Tmp += (char)PacketData[Index++]; Index++;
//			used += 2;
//		}
//
//		if (used != len)
//		{
//			Index += 2;
//		}
//	}
//	else
//	{
//		while (used < len && PacketData[Index] != 0)
//		{
//			Tmp += (char)PacketData[Index++];
//			used++;
//		}
//
//		if (used != len)
//		{
//			Index++;
//		}
//	}
//
//	return Tmp;
//}
//
//// </KEITH>
//
//public static string GetString(byte[] PacketData, ref int Index, bool IsUnicode)
//{
//	string Tmp = "";
//
//	if (IsUnicode)
//	{
//		while ((PacketData[Index] != 0) && (PacketData[Index + 1] == 0))
//		{
//			Tmp += (char)PacketData[Index++]; Index++;
//		}
//
//		Index += 2;
//	}
//	else
//	{
//		while (PacketData[Index] != 0)
//		{
//			Tmp += (char)PacketData[Index++];
//		}
//
//		Index++;
//	}
//
//	return Tmp;
//}