#include "stdafx.h"

void printUdpPacket(char *data, int length, int more)
{
	printf("Source Port: %i, Destination Port: %i\n",
		(u_char)((data[0] * 256) + data[1]), (u_char)((data[2] * 256) + data[3]));

	printf("Length: %i, Checksum: %i\n",
		(data[4] * 256) + data[5], (data[6] * 256) + data[7]);

	printRawData(data + 8, length - 8, more);
}


void writeUdpPacket(char *data, int length, int type, FILE *file1)
{
	fprintf(file1, "Source Port: %i, Destination Port: %i\n",
		(u_char)((data[0] * 256) + data[1]), (u_char)((data[2] * 256) + data[3]));

	fprintf(file1, "Length: %i, Checksum: %i\n",
		(u_char)((data[4] * 256) + data[5]), (u_char)((data[6] * 256) + data[7]));

	writeRawData(data + 8, length - 8, type, file1);
}