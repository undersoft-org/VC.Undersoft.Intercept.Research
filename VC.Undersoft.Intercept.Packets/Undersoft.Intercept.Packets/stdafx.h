// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently, but
// are changed infrequently
//

#pragma once

#include "targetver.h"

#include <stdio.h>
#include <tchar.h>

#define __WINDOWS        //   /lib/libws2_32.a
//#define __LINUX        //   -lsocket -lnsl     ??|ifconfig eth0 (-)promisc|??

#include <stdlib.h>
#include <stdio.h>
#ifdef __WINDOWS
#include <winsock2.h>
#pragma comment(lib, "ws2_32.lib")
#endif
#ifdef __LINUX
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#endif
//#include <unistd.h>  //  replaced mostly by io.h
#include <io.h>
#include <string.h>
#include <time.h>
#include "ippacket.h"

#define BUFFERSIZE 65552

#ifdef __LINUX
void closesocket(int socket) { close(socket); }
#endif



// TODO: reference additional headers your program requires here