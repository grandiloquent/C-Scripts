using QRCoder;

static void CreateQrCode()
{
	
	QRCodeGenerator qrGenerator = new QRCodeGenerator();
	QRCodeData qrCodeData = qrGenerator.CreateQrCode(ClipboardShare.GetText().Trim(), QRCodeGenerator.ECCLevel.Q);
	var data = new PngByteQRCode(qrCodeData).GetGraphic(5);
	File.WriteAllBytes("1.png".GetDesktopPath(),data);
}

CreateQrCode();
		
/*
dotnet publish -r win-x64 --self-contained false -o C:\Users\Administrator\Desktop\应用\应用 -p:PublishSingleFile=true,AssemblyName=创建二维码
*/