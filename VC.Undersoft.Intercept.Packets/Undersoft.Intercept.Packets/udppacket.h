#ifndef __UDPPACKET
#define __UDPPACKET

void printUdpPacket(char *data, int length, int more);
void writeUdpPacket(char *data, int length, int type, FILE *file1);

#endif