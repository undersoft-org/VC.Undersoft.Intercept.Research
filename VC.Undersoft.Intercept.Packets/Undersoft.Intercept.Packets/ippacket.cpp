#include "stdafx.h"
#include <string>
#include <stdio.h>
#include <sstream>



void printRawData(char *data, int length, int more)
{
	int i, c = 0;
	printf("     -------------Data Begins-------------\n");
	for (i = 0; i<length; i++)
	{
		if ((data[i]>30 && data[i]<122) ||
			(((data[i] == 10) || (data[i] == 13) || (data[i] == 123) || (data[i] == 125))
				&& (more>0)))
		{
			printf("%c", data[i]);
			c += 1;
		}
		else
		{
			printf("[%i]", data[i]);
			c += 3;
			if (data[i]>9) c++;
			if (data[i]>99) c++;
		}
		if (c >= 47)
		{
			printf("\n");
			c = 0;
		}
	}
}

void writeRawData(char *data, int length, int type, FILE *file1)
{
	int i, c = 0;
	fprintf(file1, "     -------------Data Begins-------------\n");
	for (i = 0; i<length; i++)
	{
		if ((data[i]>30 && data[i]<122) ||
			(((data[i] == 10) || (data[i] == 13) || (data[i] == 123) || (data[i] == 125))
				&& (type>0)))
		{
			fprintf(file1, "%c", (u_char)data[i]);
			c += 1;

		}
		else
		{
			fprintf(file1, "[%i]", (u_char)data[i]);
			c += 3;
			if (data[i]>9) c++;
			if (data[i]>99) c++;
		}
		if (c >= 47)
		{
			fprintf(file1, "\n");
			c = 0;
		}
	}
}

void printIpPacket(char *data, int length, int more)
{
	printf("-----------------Packet Begins-----------------\n");
	printf("IP Version: %i, Packet Size: %ibytes, Id: %i\n",
		(data[0] >> 4), (data[2] * 256) + data[3], (data[4] * 256) + data[5]);

	printf("Fragment: %i, TTL: %i, HL: %iwds, Protocol: %i\n",
		((int)(data[6] >> 4) * 256) + data[7], data[8], ((char)(data[0] << 4)) >> 4, data[9]);

	printf("Source: %i.%i.%i.%i, Destination: %i.%i.%i.%i\n",
		data[12], data[13], data[14], data[15],
		data[16], data[17], data[18], data[19]);

	if (data[9] == 6)
		printTcpPacket(data + (((char)(data[0] << 4)) >> 2), length - (((char)(data[0] << 4)) >> 2), more);
	else if (data[9] == 17)
		printUdpPacket(data + (((char)(data[0] << 4)) >> 2), length - (((char)(data[0] << 4)) >> 2), more);
	else
		printRawData(data + (((char)(data[0] << 4)) >> 2), length - (((char)(data[0] << 4)) >> 2), more);
	printf("\n------------------Packet Ends------------------\n");
}



void writeIpPacket(char *data, int length, int type)
{
FILE *file1;
char buffer[100];
char fileName[100];
int a=0;
std::string acknow;
for (a=0; a<100; a++)
fileName[a] = 0;
strcat(fileName, "data\\");
strcat(fileName, _itoa((u_char)data[12], buffer, 10));
strcat(fileName, ".");
strcat(fileName, _itoa((u_char)data[13], buffer, 10));
strcat(fileName, ".");
strcat(fileName, _itoa((u_char)data[14], buffer, 10));
strcat(fileName, ".");
strcat(fileName, _itoa((u_char)data[15], buffer, 10));
//if(data[9]==6){
//strcat(fileName, "_");
//char *data2 = data + (((u_char)(data[0] << 4)) >> 2);
//strcat(fileName, _itoa(((data2[9] * 16777216) + (data2[9] * 65536) + (data2[10] * 256) + data2[11]), buffer, 10));
//}
strcat(fileName, ".txt");



	if ((file1 = fopen(fileName, "a")) == NULL) {
		printf("\nError opening output file %s", fileName);
		return;
	}
	fprintf(file1, "-----------------Packet Begins-----------------\n");
	fprintf(file1, "IP Version: %i, Packet Size: %ibytes, Id: %i\n",
		(data[0] >> 4), (data[2] * 256) + data[3], (data[4] * 256) + data[5]);

	fprintf(file1, "Fragment: %i, TTL: %i, HL: %iwds, Protocol: %i\n",
		((int)(data[6] >> 4) * 256) + data[7], data[8], ((char)(data[0] << 4)) >> 4, data[9]);

	fprintf(file1, "Source: %i.%i.%i.%i, Destination: %i.%i.%i.%i\n",
		(u_char)data[12], (u_char)data[13], (u_char)data[14], (u_char)data[15],
		(u_char)data[16], (u_char)data[17], (u_char)data[18], (u_char)data[19]);


	if (data[9] == 6)
		writeTcpPacket(data + (((char)(data[0] << 4)) >> 2), length - (((char)(data[0] << 4)) >> 2), type, file1);
	else if (data[9] == 17)
		writeUdpPacket(data + (((char)(data[0] << 4)) >> 2), length - (((char)(data[0] << 4)) >> 2), type, file1);
	else
		writeRawData(data + (((char)(data[0] << 4)) >> 2), length - (((char)(data[0] << 4)) >> 2), type, file1);
	fprintf(file1, "\n------------------Packet Ends------------------\n\n");
	fclose(file1);

}