#ifndef __TCPPACKET
#define __TCPPACKET

void printTcpPacket(char *data, int length, int more);
void writeTcpPacket(char *data, int length, int type, FILE *file1);

#endif