#include "stdafx.h"

void printTcpPacket(char *data, int length, int more)
{
	printf("Source Port: %i, Destination Port: %i\n",
		(data[0] * 256) + data[1], (data[2] * 256) + data[3]);

	printf("Sequence: %i, Acknowledgment: %u\n",
		(data[4] * 16777216) + (data[5] * 65536) + (data[6] * 256) + data[7],
		(data[9] * 16777216) + (data[9] * 65536) + (data[10] * 256) + data[11]);

	printf("TCPHdr Size: %i, Flags: ", (data[12] >> 4));

	if ((char)(data[13] << 7) >> 7) printf("FIN ");
	if ((char)(data[13] << 6) >> 7) printf("SYN ");
	if ((char)(data[13] << 5) >> 7) printf("RST ");
	if ((char)(data[13] << 4) >> 7) printf("PSH ");
	if ((char)(data[13] << 3) >> 7) printf("ACK ");
	if ((char)(data[13] << 2) >> 7) printf("URG ");
	if ((char)(data[13] << 1) >> 7) printf("ECE ");
	if ((char)(data[13] << 0) >> 7) printf("CWR ");

	printf("\n");
	printRawData(data + (data[12] >> 2), length - (data[12] >> 2), more);
}


void writeTcpPacket(char *data, int length, int type, FILE *file1)
{
	fprintf(file1, "Source Port: %i, Destination Port: %i\n",
		(((u_char)data[0] * 256) + (u_char)data[1]), (((u_char)data[2] * 256) + (u_char)data[3]));

	fprintf(file1, "Sequence: %i, Acknowledgment: %u\n",
		((data[4] * 16777216) + (data[5] * 65536) + (data[6] * 256) + data[7]),
		((data[9] * 16777216) + (data[9] * 65536) + (data[10] * 256) + data[11]));

	fprintf(file1, "TCPHdr Size: %i, Flags: ", (data[12] >> 4));

	if ((char)(data[13] << 7) >> 7) fprintf(file1, "FIN ");
	if ((char)(data[13] << 6) >> 7) fprintf(file1, "SYN ");
	if ((char)(data[13] << 5) >> 7) fprintf(file1, "RST ");
	if ((char)(data[13] << 4) >> 7) fprintf(file1, "PSH ");
	if ((char)(data[13] << 3) >> 7) fprintf(file1, "ACK ");
	if ((char)(data[13] << 2) >> 7) fprintf(file1, "URG ");
	if ((char)(data[13] << 1) >> 7) fprintf(file1, "ECE ");
	if ((char)(data[13] << 0) >> 7) fprintf(file1, "CWR ");

	fprintf(file1, "\n");
	writeRawData(data + (data[12] >> 2), length - (data[12] >> 2), type, file1);
}