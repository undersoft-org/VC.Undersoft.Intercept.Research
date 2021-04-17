#include "stdafx.h"
#include "stdio.h"
#include <winsock.h>

 
int main(int argc, char *argv[])
{
	int thisSocket, optVal = 1, newData, result, packetCount;
	int inn = 1, outt, more = 0;
	long rett;
	time_t nowTime;
	struct sockaddr_in destination;
	char packetBuffer[BUFFERSIZE];
    int port = atoi(argv[2]);

#ifdef __WINDOWS
	WSADATA wsaData;
#endif

	printf("Packet Interceptor!");
	//**************************************
	if (argc <= 2)
	{
		printf("\nUseage...");
		printf("\n [IP-address] [port] [packet-count] (ml) (o)");		
		return EXIT_SUCCESS;
	}

#ifdef __WINDOWS
	WSAStartup(0x0202, &wsaData);
#endif
	//**********************************
	thisSocket = socket(AF_INET, SOCK_RAW, IPPROTO_IP);
	if (thisSocket < 0)
	{
		printf("Socket creation FAILED!");
		if (thisSocket) closesocket(thisSocket);
		return 0;
	}
	printf("Socket created!");
	//**********************************
#ifdef __WINDOWS	
	if (setsockopt(thisSocket, IPPROTO_IP, 2, (char *)&optVal, sizeof(optVal))<0)
	{
		printf("\nUnable to set socket options!");
		if (thisSocket) closesocket(thisSocket);
		return 0;
	}
	printf("\nOptions set!");
#endif
	//**********************************
	//destination.sin_family = socket(AF_INET, SOCK_RAW, IPPROTO_RAW);
	destination.sin_family = AF_INET;
	destination.sin_port = 0;
	destination.sin_addr.s_addr = inet_addr(argv[1]);
	if (bind(thisSocket, (struct sockaddr *)&destination, sizeof(destination))<0) {
		printf("\nBinding Socket FAILED!\n");
	//	if (thisSocket) close(thisSocket);
	//	return 0;
	}
	printf("\nSocket bound to %s!", argv[1]);
	//**********************************
#ifdef __WINDOWS
	if (WSAIoctl(thisSocket, 0x98000001, &inn, sizeof(inn), &outt, sizeof(outt), (LPDWORD)&rett, 0, 0) != 0)
	{
		printf("\nCouldn't set IO control!\n");
		if (thisSocket) closesocket(thisSocket);
		return 0;
	}
	printf("\nIO controls set!");
#endif
	//**********************************
	if ((argc >= 5) && (strcmp(argv[4], "m") == 0))
		more = 1;
	//**********************************
	result = atoi(argv[3]);
	packetCount = 0;
	printf("\nWaiting for %i packets...\n", result);
	while (packetCount < result || result == 0)
	{
		newData = recv(thisSocket, packetBuffer, BUFFERSIZE, 0);
		char* datac = packetBuffer + (((char)(packetBuffer[0] << 4)) >> 2);
		int s = (((u_char)datac[0] * 256) + (u_char)datac[1]);
		int d = (((u_char)datac[2] * 256) + (u_char)datac[3]);
		printf("s_%i - ", (((u_char)datac[0] * 256) + (u_char)datac[1]));
		printf("d_%i - ", (((u_char)datac[2] * 256) + (u_char)datac[3]));
		printf("cs_%i - cd_%i \n ", s, d);
		if (s == port || d == port)
		{
			time(&nowTime);
			printf("\n\nPacket %i: at %u\n", packetCount, (unsigned int)nowTime);
			//printIpPacket(packetBuffer, newData, more);

			writeIpPacket(packetBuffer, newData, more);
			packetCount++;
		}
	}
	//**********************************
	closesocket(thisSocket);
#ifdef __WINDOWS
	system("PAUSE");
#endif
	return 0;
}