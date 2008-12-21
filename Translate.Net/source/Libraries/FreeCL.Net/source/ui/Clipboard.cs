#region License block : MPL 1.1/GPL 2.0/LGPL 2.1
/* ***** BEGIN LICENSE BLOCK *****
 * Version: MPL 1.1/GPL 2.0/LGPL 2.1
 *
 * The contents of this file are subject to the Mozilla Public License Version
 * 1.1 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 * http://www.mozilla.org/MPL/
 *
 * Software distributed under the License is distributed on an "AS IS" basis,
 * WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
 * for the specific language governing rights and limitations under the
 * License.
 *
 * The Original Code is the FreeCL.Net library.
 *
 * The Initial Developer of the Original Code is 
 *  Oleksii Prudkyi (Oleksii.Prudkyi@gmail.com).
 * Portions created by the Initial Developer are Copyright (C) 2005-2008
 * the Initial Developer. All Rights Reserved.
 *
 * Contributor(s):
 *
 * Alternatively, the contents of this file may be used under the terms of
 * either the GNU General Public License Version 2 or later (the "GPL"), or
 * the GNU Lesser General Public License Version 2.1 or later (the "LGPL"),
 * in which case the provisions of the GPL or the LGPL are applicable instead
 * of those above. If you wish to allow use of your version of this file only
 * under the terms of either the GPL or the LGPL, and not to allow others to
 * use your version of this file under the terms of the MPL, indicate your
 * decision by deleting the provisions above and replace them with the notice
 * and other provisions required by the GPL or the LGPL. If you do not delete
 * the provisions above, a recipient may use your version of this file under
 * the terms of any one of the MPL, the GPL or the LGPL.
 *
 * ***** END LICENSE BLOCK ***** */
 /*
  * Clipboard backup code originally implemented by Alessio Deiana
  * http://secure.codeproject.com/KB/system/clipboard_backup_cs.aspx
  * Changes : CLS-Compliance
  * */
#endregion

 
using System;
using FreeCL.RTL;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Collections.Generic;

namespace FreeCL.UI
{
	/// <summary>
	/// Description of Clipboard.	
	/// </summary>
	public static class Clipboard
	{
	
		public delegate W FunctionWithReturn<W>();
		public delegate void FunctionWithoutReturn();
		/// <summary>
		/// Does application-wide checking
		/// </summary>
		/// 
		public static bool CanCopy
		{
			get
			{
				
				Control activeControl = Application.ActiveControl;
				if(activeControl == null)
				{
					return false;
				}

				System.Windows.Forms.TextBoxBase active = activeControl as System.Windows.Forms.TextBoxBase;	
				if(active != null)
				{
					return active.SelectionLength > 0;
				}
				
				System.Windows.Forms.WebBrowser webbrowser = activeControl as System.Windows.Forms.WebBrowser;	
				if(webbrowser != null && WebBrowserHelper.GetDocument(webbrowser) != null)
				{  
					return true;
				}

				
				PropertyInfo property = activeControl.GetType().GetProperty("SelectionLength", typeof(int));
				if(property != null)
				{
					MethodInfo method = property.GetGetMethod();
					if(method != null)
					{
						FunctionWithReturn<int> getMethod = (FunctionWithReturn<int>) Delegate.CreateDelegate
            				(typeof(FunctionWithReturn<int>), activeControl, method);
            				
            			return getMethod() > 0;	
					}
				}
				
				return false;
			}
		}
		
		/// <summary>
		/// Does application-wide copy
		/// </summary>
		public static void Copy()
		{
			Control activeControl = Application.ActiveControl;
			if(activeControl == null)
			{
				return;
			}
		
			System.Windows.Forms.TextBoxBase active = activeControl as System.Windows.Forms.TextBoxBase;	
			if(active != null)
			{
				active.Copy();
				return;
			}

			System.Windows.Forms.WebBrowser webbrowser = activeControl as System.Windows.Forms.WebBrowser;	
			if(webbrowser != null)
			{  
				WebBrowserHelper.GetDocument(webbrowser).ExecCommand("Copy", false, null);
				return;
			}
			
			MethodInfo method = activeControl.GetType().GetMethod("Copy");
			if(method != null)
			{
				FunctionWithoutReturn getMethod = (FunctionWithoutReturn) Delegate.CreateDelegate
            		(typeof(FunctionWithoutReturn), activeControl, method);
            				
            	getMethod();	
			}
			
		}

		/// <summary>
		/// Does application-wide cut
		/// </summary>
		public static void Cut()
		{
			Control activeControl = Application.ActiveControl;
			if(activeControl == null)
			{
				return;
			}
		
			System.Windows.Forms.TextBoxBase active = activeControl as System.Windows.Forms.TextBoxBase;	
			if(active != null)
			{
				active.Cut();
				return;
			}
			
			System.Windows.Forms.WebBrowser webbrowser = activeControl as System.Windows.Forms.WebBrowser;	
			if(webbrowser != null)
			{  
				WebBrowserHelper.GetDocument(webbrowser).ExecCommand("Copy", false, null);
				return;
			}
			
			MethodInfo method = activeControl.GetType().GetMethod("Cut");
			if(method != null)
			{
				FunctionWithoutReturn getMethod = (FunctionWithoutReturn) Delegate.CreateDelegate
            		(typeof(FunctionWithoutReturn), activeControl, method);
            				
            	getMethod();	
			}
			
		}

		/// <summary>
		/// Does application-wide checking
		/// </summary>
		public static bool CanPaste
		{
			get
			{
			
				Control activeControl = Application.ActiveControl;
				if(activeControl == null)
				{
					return false;
				}
			
				System.Windows.Forms.TextBoxBase active = activeControl as System.Windows.Forms.TextBoxBase;	
				if(active != null)
				{
					IDataObject iData = null;
					try
					{
						iData = System.Windows.Forms.Clipboard.GetDataObject();
					}
					catch(System.Runtime.InteropServices.ExternalException)
					{
					
					}
					
					if(iData == null)
						return false;
				
					System.Windows.Forms.RichTextBox richText = activeControl as System.Windows.Forms.RichTextBox;
					if(richText != null)
					{
						if(richText.ReadOnly)
							return false;
							
						string[] formats = iData.GetFormats(true);
						bool canPaste = false;
						foreach(string format in formats)
						{
							canPaste = richText.CanPaste(DataFormats.GetFormat(format));
							if(canPaste)
								break;
						}
						return canPaste;
					}	
					else
					{
						return !active.ReadOnly && iData.GetDataPresent(DataFormats.Text);
					}
				}
				
				MethodInfo method = activeControl.GetType().GetMethod("CanPaste");
				if(method != null)
				{
					FunctionWithReturn<bool> getMethod = (FunctionWithReturn<bool>) Delegate.CreateDelegate
            				(typeof(FunctionWithReturn<bool>), activeControl, method);
            				
           			return getMethod();	
				}
				
				
				return false;
			}
		}

		/// <summary>
		/// Does application-wide cut
		/// </summary>
		public static void Paste()
		{
			Control activeControl = Application.ActiveControl;
			if(activeControl == null)
			{
				return;
			}
		
			System.Windows.Forms.TextBoxBase active = activeControl as System.Windows.Forms.TextBoxBase;	
			if(active != null)
			{
				active.Paste();
				return;
			}
			
			MethodInfo method = activeControl.GetType().GetMethod("Paste");
			if(method != null)
			{
				FunctionWithoutReturn getMethod = (FunctionWithoutReturn) Delegate.CreateDelegate
            		(typeof(FunctionWithoutReturn), activeControl, method);
            				
            	getMethod();	
			}
		}
		
		static class NativeMethods
		{
	       [DllImport("user32.dll")]
	        public static extern bool OpenClipboard(IntPtr hWndNewOwner);
	
	        [DllImport("user32.dll")]
	        public static extern bool EmptyClipboard();
	
	        [DllImport("user32.dll")]
	        public static extern IntPtr GetClipboardData(uint uFormat);
	
	        [DllImport("user32.dll")]
	        public static extern IntPtr SetClipboardData(uint uFormat, IntPtr hMem);
	
	        [DllImport("user32.dll")]
	        public static extern bool CloseClipboard();
	
	        [DllImport("user32.dll")]
	        public static extern uint EnumClipboardFormats(uint format);
	
	        [DllImport("user32.dll")]
	        public static extern int GetClipboardFormatName(uint format, [Out] StringBuilder lpszFormatName, int cchMaxCount);
	
	        [DllImport("user32.dll", SetLastError = true)]
	        public static extern uint RegisterClipboardFormat(string lpszFormat);		
	        
 			[DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory", SetLastError = false)]
	        public static extern void CopyMemory(IntPtr dest, IntPtr src, int size);
	
	        [DllImport("kernel32.dll")]
	        public static extern IntPtr GlobalAlloc(uint uFlags, IntPtr dwBytes);
	
	        [DllImport("kernel32.dll")]
	        public static extern IntPtr GlobalLock(IntPtr hMem);
	
	        [DllImport("kernel32.dll")]
	        public static extern IntPtr GlobalUnlock(IntPtr hMem);
	
	        [DllImport("kernel32.dll")]
	        public static extern IntPtr GlobalFree(IntPtr hMem);
	
	        [DllImport("kernel32.dll")]
	        public static extern UIntPtr GlobalSize(IntPtr hMem);
	
	        public const uint GMEM_DDESHARE = 0x2000;
	        public const uint GMEM_MOVEABLE = 0x2;	        
		}
		
		static ReadOnlyCollection<DataClip> clipBackupData;
		
		public static void BackupClipboard()
		{
			clipBackupData = GetClipboard();
		}
		
		public static void RestoreClipboard()
		{
			if(clipBackupData != null)
			{
				SetClipboard(clipBackupData);
				clipBackupData = null;
			}	
		}
		
        /// <summary>
        /// Remove all data from Clipboard
        /// </summary>
        /// <returns></returns>
        public static bool EmptyClipboard()
        {
        	if (!NativeMethods.OpenClipboard(IntPtr.Zero))
            	return false;
			bool result = NativeMethods.EmptyClipboard();
			NativeMethods.CloseClipboard();
			return result;
        }

        /// <summary>
        /// Empty the Clipboard and Restore to system clipboard data contained in a collection of ClipData objects
        /// </summary>
        /// <param name="clipData">The collection of ClipData containing data stored from clipboard</param>
        /// <returns></returns>    
        public static bool SetClipboard(ReadOnlyCollection<DataClip> clipData)
        {
            //Open clipboard to allow its manipultaion
            if (!NativeMethods.OpenClipboard(IntPtr.Zero))
                return false;
            
            //Clear the clipboard
            NativeMethods.EmptyClipboard();
                        
            //Get an Enumerator to iterate into each ClipData contained into the collection
            IEnumerator<DataClip> cData = clipData.GetEnumerator();
            while( cData.MoveNext())
            {
                DataClip cd = cData.Current;

                //Get the pointer for inserting the buffer data into the clipboard
                IntPtr alloc = NativeMethods.GlobalAlloc(NativeMethods.GMEM_MOVEABLE | NativeMethods.GMEM_DDESHARE, cd.Size);
                IntPtr gLock = NativeMethods.GlobalLock(alloc);

                //Clopy the buffer of the ClipData into the clipboard
                if ((int)cd.Size>0)
                {
                    Marshal.Copy(cd.Buffer, 0, gLock, cd.Buffer.GetLength(0));
                }
                else
                {
                }
                //Release pointers 
                NativeMethods.GlobalUnlock(alloc);
                NativeMethods.SetClipboardData((uint)cd.Format, alloc);
            };
            //Close the clipboard to realese unused resources
            NativeMethods.CloseClipboard();
            return true;
        }

        /// <summary>
        /// Save a collection of ClipData to HardDisk
        /// </summary>
        /// <param name="clipData">The collection of ClipData to save</param>
        /// <param name="fileName">The name of the file</param>
        public static void SaveToFile(ReadOnlyCollection<DataClip> clipData, string clipName)
        {
            //Get the enumeration of the clipboard data
            IEnumerator<DataClip> cData = clipData.GetEnumerator();
            //Init a counter
            int i = 0;
            //Delete the folder, if already exists
            if (Directory.Exists(clipName))
            {
                Directory.Delete(clipName,true);
            }
            //Open the directory on which save the files
            DirectoryInfo di= Directory.CreateDirectory(clipName);
            
            while (cData.MoveNext())
            {
                //Init the serializer
                XmlSerializer xml = new XmlSerializer(typeof(DataClip));
                // To write to a file, create a StreamWriter object.
                using (StreamWriter sw = new StreamWriter(di.FullName + @"\" + i.ToString() + ".cli",false))
                {
                    //Serialize the clipboard data
                    xml.Serialize(sw, cData.Current);
                }
                
                i++;
            }

            

         }
		/// <summary>
		/// Open the file and deserialize the collection of DataClips
		/// </summary>
		/// <param name="fileName">The path of the file to open</param>
		/// <returns></returns>
        private static ReadOnlyCollection<DataClip> ReadFromFile(string clipName)
        {
            //Init the List to return as result
            List<DataClip> clips = new List<DataClip>();
            //Check if the clip exists on hd
            if (Directory.Exists(clipName))
            {
                DirectoryInfo di = new DirectoryInfo(clipName);

                //Loop for each clipboard data
                for (int x = 0; x < di.GetFiles().GetLength(0); x++)
                {
                    //Init the serializer
                    XmlSerializer xml = new XmlSerializer(typeof(DataClip));
                    //Set the file to read
                    FileInfo fi = new FileInfo(di.FullName + "\\" + x.ToString() + ".cli");
                    //Init the stream to deserialize
                    using (FileStream fs = fi.Open(FileMode.Open))
                    {
                        //deserialize and add to the List
                        clips.Add((DataClip)xml.Deserialize(fs));
                    }
                }
            }
            return new ReadOnlyCollection<DataClip>( clips);    
        }
            

        /// <summary>
        /// Get data from clipboard and save it to Hard Disk
        /// </summary>
        /// <param name="fileName">The name of the file</param>
        public static void Serialize(string clipName)
        {
            //Get data from clipboard
            ReadOnlyCollection<DataClip> clipData = GetClipboard();
            //Save data to hard disk
            SaveToFile(clipData, clipName);
        }

        /// <summary>
        /// Get data from hard disk and put them into the clipboard
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool Deserialize(string clipName)
        {
            //Get data from hard disk
            ReadOnlyCollection<DataClip> clipData = ReadFromFile(clipName);
            //Set red data into clipboard
            return SetClipboard(clipData);
        }
        /// <summary>
        /// Convert to a DataClip collection all data present in the clipboard
        /// </summary>
        /// <returns></returns>
        public static ReadOnlyCollection<DataClip> GetClipboard()
        {
            //Init a list of ClipData, which will contain each Clipboard Data
            List<DataClip> clipData = new List<DataClip>();

            //Open Clipboard to allow us to read from it
            if (!NativeMethods.OpenClipboard(IntPtr.Zero))
                return new ReadOnlyCollection<DataClip>(clipData);

            //Loop for each clipboard data type
            int format = 0;
            while ((format = (int)NativeMethods.EnumClipboardFormats((uint)format)) != 0)
            {
                //Check if clipboard data type is recognized, and get its name
                string formatName = "0";
                DataClip cd;
                if (format > 14)
                {
                    StringBuilder res = new StringBuilder();
                    if (NativeMethods.GetClipboardFormatName((uint)format, res, 100) > 0)
                    {
                        formatName = res.ToString();
                    }

                }
                    //Get the pointer for the current Clipboard Data 
                    IntPtr pos = NativeMethods.GetClipboardData((uint)format);
                    //Goto next if it's unreachable
                    if (pos == IntPtr.Zero)
                        continue;
                    //Get the clipboard buffer data properties
                    UIntPtr lenght = NativeMethods.GlobalSize(pos);
                    IntPtr gLock = NativeMethods.GlobalLock(pos);
                    byte[] buffer;
                    if ((int)lenght > 0)
                    {
                        //Init a buffer which will contain the clipboard data
                        buffer = new byte[(int)lenght];
                        int l = Convert.ToInt32(lenght.ToString());
                        //Copy data from clipboard to our byte[] buffer
                        Marshal.Copy(gLock, buffer, 0, l);
                    }
                    else
                    {
                        buffer = new byte[0];
                    }
                    //Create a ClipData object that represtens current clipboard data
                    cd = new DataClip(format, formatName, buffer);
                    cd.FormatName = formatName;
                    //Add current Clipboard Data to the list
                    
                
                clipData.Add(cd);
            }
            //Close the clipboard and realese unused resources
            NativeMethods.CloseClipboard();
            //Returns the list of Clipboard Datas as a ReadOnlyCollection of ClipData
            return new ReadOnlyCollection<DataClip>(clipData);
        }
	}
	
	/// <summary>
    /// Holds clipboard data of a single data format.
    /// </summary>
    [Serializable]
    public class DataClip
    {
        private uint format;

        /// <summary>
        /// Get or Set the format code of the data 
        /// </summary>
        public int Format
        {
            get { return (int)format; }
            set { format = (uint)value; }
        }
        
        private string formatName;
        /// <summary>
        /// Get or Set the format name of the data 
        /// </summary>
        public string FormatName
        {
            get { return formatName; }
            set { formatName = value; }
        }
        private byte[] buffer;

        private int size;

        /// <summary>
        /// Get or Set the buffer data
        /// </summary>
        public byte[] Buffer
        {
            get { return buffer; }
            set { buffer = value; }
        }
        /// <summary>
        /// Get the data buffer lenght
        /// </summary>
        public IntPtr Size
        {
            get
            {
                if (buffer != null)
                {
                    //Read the correct size from buffer, if it is not null
                    return new IntPtr(buffer.GetLength(0));
                }
                else
                {
                    //else return the optional set size
                    return new IntPtr(size);
                }
            }
        }
        /// <summary>
        /// Init a Clip Data object, containing one clipboard data and its format
        /// </summary>
        /// <param name="format"></param>
        /// <param name="formatName"></param>
        /// <param name="buffer"></param>
        public DataClip(int format, string formatName, byte[] buffer)
        {
            this.format = (uint)format;
            this.formatName = formatName;
            this.buffer = buffer;
            this.size = 0;
        }
		/// <summary>
		/// Init an empty Clip Data object, used for serialize object
		/// </summary>
        public DataClip() { }
    }	
}
