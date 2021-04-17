#ifndef __IPPACKET
#define __IPPACKET

#include "tcppacket.h"
#include "udppacket.h"

void printRawData(char *data, int length, int more);
void writeRawData(char *data, int length, int type, FILE *file1);
void printIpPacket(char *data, int length, int more);
void writeIpPacket(char *data, int length, int more);

#endif