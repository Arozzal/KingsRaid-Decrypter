using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using MiniJSON;
using NShared.NUtil;

namespace kingsparsetest
{
    class Program
    {
        static object encryptFile(string filePath)
        {
            byte[] encryptedData = File.ReadAllBytes(filePath);
            RijndaelManaged rin = new RijndaelManaged();
            rin.Mode = CipherMode.CBC;
            rin.Padding = PaddingMode.PKCS7;
            rin.KeySize = 128;
            rin.BlockSize = 128;
            byte[] arr = Convert.FromBase64String("rta5TAu4GPffXFR4S2bCjg==");
            byte[] arr2 = new byte[16];
            int num = arr.Length;
            if (num > arr2.Length)
            {
                num = arr2.Length;
            }
            Array.Copy(arr, arr2, num);
            rin.Key = arr2;
            rin.IV = arr2;
            byte[] decryptedBytes = rin.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            return MiniJSON.JsonBin.Deserialize(decryptedBytes) as List<object>;
        }

        static byte[] decryptFile(object obj)
        {
            byte[] data = MiniJSON.JsonBin.Serialize(obj);

            RijndaelManaged rin = new RijndaelManaged();
            rin.Mode = CipherMode.CBC;
            rin.Padding = PaddingMode.PKCS7;
            rin.KeySize = 128;
            rin.BlockSize = 128;
            byte[] arr = Convert.FromBase64String("rta5TAu4GPffXFR4S2bCjg==");
            byte[] arr2 = new byte[16];
            int num = arr.Length;
            if (num > arr2.Length)
            {
                num = arr2.Length;
            }
            Array.Copy(arr, arr2, num);
            rin.Key = arr2;
            rin.IV = arr2;

            byte[] data2 = rin.CreateEncryptor().TransformFinalBlock(data, 0, data.Length);

            return data2;
        }


        static object encryptLitFile(string filePath)
        {
            byte[] encryptedData = File.ReadAllBytes(filePath);
            RijndaelManaged rin = new RijndaelManaged();
            rin.Mode = CipherMode.CBC;
            rin.Padding = PaddingMode.PKCS7;
            rin.KeySize = 128;
            rin.BlockSize = 128;
            byte[] arr = Convert.FromBase64String("rta5TAu4GPffXFR4S2bCjg==");
            byte[] arr2 = new byte[16];
            int num = arr.Length;
            if (num > arr2.Length)
            {
                num = arr2.Length;
            }
            Array.Copy(arr, arr2, num);
            rin.Key = arr2;
            rin.IV = arr2;
            byte[] decryptedBytes = rin.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            return MiniJSON.JsonBin.Deserialize(decryptedBytes) as List<object>;
        }

        static string encryptDecompressed(string filePath)
        {
            byte[] encryptedData = Convert.FromBase64String(File.ReadAllText(filePath));
            RijndaelManaged rin = new RijndaelManaged();
            rin.Mode = CipherMode.CBC;
            rin.Padding = PaddingMode.PKCS7;
            rin.KeySize = 128;
            rin.BlockSize = 128;
            byte[] arr = Convert.FromBase64String("rta5TAu4GPffXFR4S2bCjg==");
            byte[] arr2 = new byte[16];
            int num = arr.Length;
            if (num > arr2.Length)
            {
                num = arr2.Length;
            }
            Array.Copy(arr, arr2, num);
            rin.Key = arr2;
            rin.IV = arr2;
            byte[] data = rin.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            return Lz4.Decompress(Encoding.UTF8.GetString(data));
            
        }



        static void writeFile(List<object> table, Dictionary<int, string> nameList, string saveName)
        {
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(saveName), true))
            {
                foreach (Dictionary<string, object> js in table)
                {
                    outputFile.WriteLine("");
                    for (int i = 0; i < js.Count; i++)
                    {
                        outputFile.Write(js.Keys.ElementAt(i) + ": \t");

                        
                        if(js.Values.ElementAt(i).GetType() == typeof(string) || js.Values.ElementAt(i).GetType() == typeof(int) ||
                           js.Values.ElementAt(i).GetType() == typeof(float) || js.Values.ElementAt(i).GetType() == typeof(double) ||
                           js.Values.ElementAt(i).GetType() == typeof(long)) 
                        {
                            outputFile.WriteLine(js.Values.ElementAt(i));
                        }
                        else
                        {
                            List<object> ls = js.Values.ElementAt(i) as List<object>;
                            outputFile.WriteLine("[");
                            for(int j = 0; j < ls.Count; j++)
                            {
                                outputFile.WriteLine("\t" + ls[j] + ",");
                            }
                            outputFile.WriteLine("]");

                        }
                        

                    }
                    /* int cindex = Convert.ToInt32(js["ItemIndex"]);
                     if (cindex != currentItem)
                     {
                         outputFile.WriteLine();
                         outputFile.WriteLine(nameList[cindex]);
                         currentItem = cindex;
                     }*/
                    //outputFile.WriteLine(js["ComponentRate"] + " " + js["ComponentCount"]);





                }

            }
        }




        static void writeAllFiles(Dictionary<int, string> names)
        {
            string[] files = Directory.GetFiles("TableData");

            for(int i = 0; i < files.Length; i++)
            {
                /*if(files[0][files]] == '')
                {

                }

                //writeFile(encryptFile(files[0]), names, "Ta);

    */
            }


        }

        /*
         
             if (ds["Index"].ToString() == "46")
                {
                    ds["BaseSkillIndex"] = 550100;
                    ds["SkillIndex1"] = 550110;
                    ds["SkillIndex2"] = 550140;
                    ds["SkillIndex3"] = 550130;
                    ds["SkillIndex4"] = 550120;
                }

                if (ds["Index"].ToString() == "15")
                {
                    ds["BaseSkillIndex"] = 550200;
                    ds["SkillIndex1"] = 550210;
                    ds["SkillIndex2"] = 550220;
                    ds["SkillIndex3"] = 550230;
                    ds["SkillIndex4"] = 550240;
                }

                if (ds["Index"].ToString() == "2")
                {
                    ds["BaseSkillIndex"] = 550300;
                    ds["SkillIndex1"] = 550310;
                    ds["SkillIndex2"] = 550320;
                    ds["SkillIndex3"] = 550330;
                    ds["SkillIndex4"] = 550340;
                }
             */



        static void Main(string[] args)
        {

            //List<object> nameList = encryptFile("TableData/ItemGroupTable.bvo") as List<object>;
            //List<object> probabilityList = encryptFile("TableData/ItemProbabilityTable.bvo") as List<object>;



            List<object> testTable = encryptFile("RewardTable1.bvo") as List<object>;

                    for (int i = 0; i < testTable.Count; i++)
                     {
                         Dictionary<string, object> ds = testTable[i] as Dictionary<string, object>;


                         //Orpe
                         if (ds["Index"].ToString() == "4600")
                         {
                            System.Collections.Generic.List<Object> l = ds["OperationValue1"] as List<Object>;
                            l[0] = 460000000000;
                            ds["OperationValue1"] = l;



                        continue;
                        }

                    if (ds["Index"].ToString() == "4610")
                    {
                        System.Collections.Generic.List<Object> l = ds["OperationValue1"] as List<Object>;
                    l[0] = 460000000000;
                    l[1] = 460000000000;
                    l[2] = 460000000000;
                    ds["OperationValue1"] = l;

                    System.Collections.Generic.List<Object> s = ds["OperationValue3"] as List<Object>;
                    s[0] = 100000;
                    ds["OperationValue3"] = s;


                    continue;
                    }

            }

                     byte[] da = decryptFile(testTable);

                     File.WriteAllBytes("SkillTable3.bvo2", da);
                     

            Dictionary<int, string> names = new Dictionary<int, string>();

        

            /*foreach(Dictionary<string, object> js in nameList)
            {
                
                int index = Convert.ToInt32(js["Index"]);
                string name = Convert.ToString(js["Code"]);
                names.Add(index, name);
            }*/

            //writeAllFiles(names);
            writeFile(testTable, names, "now.txt");
            //File.WriteAllText("nows.txt", testTable);

            // File.WriteAllText("testsss.json", jsons);

        }
    }
}
