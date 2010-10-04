import System
import System.IO
import Boo.Lang

directory = "C:\\$Code\\Temp\\Env"

file_path = Path.Combine(directory, "vendor-file.txt")

#check for temp directory
if not Directory.Exists(directory):
	results.Add(fail("Temp directory does not exist"))
elif not File.Exists(file_path):
	results.Add(fail("Vendor file does not exist"))	
else:

	sr = StreamReader(file_path)
	file_data = sr.ReadToEnd()
	file_data_len = len(file_data)
	lines = file_data.Split("\n"[0])
	sr.Dispose()	
	
	for line in lines:
		data = line.Split("|"[0])
		
		if len(data) != 3:
			results.Add(fail("Invalid data count on line"))
			break
		else:
			if data[2].Contains("$"):
				results.Add(warn("$ found in price, file will be updated"))
				file_data = file_data.Replace(line, line.Replace(data[2], data[2].Replace("$", "")))								
				
	if len(file_data) != file_data_len:
		sw = StreamWriter(file_path, false)
		sw.Write(file_data)
		sw.Dispose()
		
	
	
	
	
	