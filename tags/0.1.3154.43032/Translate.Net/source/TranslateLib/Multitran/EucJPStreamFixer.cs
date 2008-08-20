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
 * Portions created by the Initial Developer are Copyright (C) 2008
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
#endregion


using System;
using System.IO; 
using System.Collections.Generic;

namespace Translate
{
	/// <summary>
	/// Description of EucJPStreamFixer.
	/// </summary>
	internal class EucJPStreamFixer : StreamConvertor
	{
		public EucJPStreamFixer()
		{
		}
		
		public override System.IO.Stream ConvertStream(System.IO.Stream stream)
		{
			return new EucJPFixerStream(stream);
		}
	}
	
	internal class EucJPFixerStream : Stream
	{
		public EucJPFixerStream(Stream parent)
		{
			this.parent = parent;
		}
		System.IO.Stream parent;
		
		static EucJPFixerStream()
		{
			//http://unicode.org/Public/MAPPINGS/OBSOLETE/EASTASIA/JIS/JIS0208.TXT
			//http://unicode.org/Public/MAPPINGS/VENDORS/APPLE/CHINSIMP.TXT
			//recode cyrillic from shift-JIS to euc-jp
			convertJISToEucDictionary.Add(0x40, 0xA1	);  //# CYRILLIC CAPITAL LETTER A            
			convertJISToEucDictionary.Add(0x41, 0xA2	);  //# CYRILLIC CAPITAL LETTER BE           
			convertJISToEucDictionary.Add(0x42, 0xA3	);  //# CYRILLIC CAPITAL LETTER VE           
			convertJISToEucDictionary.Add(0x43, 0xA4	);  //# CYRILLIC CAPITAL LETTER GHE          
			convertJISToEucDictionary.Add(0x44, 0xA5	);  //# CYRILLIC CAPITAL LETTER DE           
			convertJISToEucDictionary.Add(0x45, 0xA6	);  //# CYRILLIC CAPITAL LETTER IE           
			convertJISToEucDictionary.Add(0x46, 0xA7	);  //# CYRILLIC CAPITAL LETTER IO           
			convertJISToEucDictionary.Add(0x47, 0xA8	);  //# CYRILLIC CAPITAL LETTER ZHE          
			convertJISToEucDictionary.Add(0x48, 0xA9	);  //# CYRILLIC CAPITAL LETTER ZE           
			convertJISToEucDictionary.Add(0x49, 0xAA	);  //# CYRILLIC CAPITAL LETTER I            
			convertJISToEucDictionary.Add(0x4A, 0xAB	);  //# CYRILLIC CAPITAL LETTER SHORT I      
			convertJISToEucDictionary.Add(0x4B, 0xAC	);  //# CYRILLIC CAPITAL LETTER KA           
			convertJISToEucDictionary.Add(0x4C, 0xAD	);  //# CYRILLIC CAPITAL LETTER EL           
			convertJISToEucDictionary.Add(0x4D, 0xAE	);  //# CYRILLIC CAPITAL LETTER EM           
			convertJISToEucDictionary.Add(0x4E, 0xAF	);  //# CYRILLIC CAPITAL LETTER EN           
			convertJISToEucDictionary.Add(0x4F, 0xB0	);  //# CYRILLIC CAPITAL LETTER O            
			convertJISToEucDictionary.Add(0x50, 0xB1	);  //# CYRILLIC CAPITAL LETTER PE           
			convertJISToEucDictionary.Add(0x51, 0xB2	);  //# CYRILLIC CAPITAL LETTER ER           
			convertJISToEucDictionary.Add(0x52, 0xB3	);  //# CYRILLIC CAPITAL LETTER ES           
			convertJISToEucDictionary.Add(0x53, 0xB4	);  //# CYRILLIC CAPITAL LETTER TE           
			convertJISToEucDictionary.Add(0x54, 0xB5	);  //# CYRILLIC CAPITAL LETTER U            
			convertJISToEucDictionary.Add(0x55, 0xB6	);  //# CYRILLIC CAPITAL LETTER EF           
			convertJISToEucDictionary.Add(0x56, 0xB7	);  //# CYRILLIC CAPITAL LETTER HA           
			convertJISToEucDictionary.Add(0x57, 0xB8	);  //# CYRILLIC CAPITAL LETTER TSE          
			convertJISToEucDictionary.Add(0x58, 0xB9	);  //# CYRILLIC CAPITAL LETTER CHE          
			convertJISToEucDictionary.Add(0x59, 0xBA	);  //# CYRILLIC CAPITAL LETTER SHA          
			convertJISToEucDictionary.Add(0x5A, 0xBB	);  //# CYRILLIC CAPITAL LETTER SHCHA        
			convertJISToEucDictionary.Add(0x5B, 0xBC	);  //# CYRILLIC CAPITAL LETTER HARD SIGN    
			convertJISToEucDictionary.Add(0x5C, 0xBD	);  //# CYRILLIC CAPITAL LETTER YERU         
			convertJISToEucDictionary.Add(0x5D, 0xBE	);  //# CYRILLIC CAPITAL LETTER SOFT SIGN    
			convertJISToEucDictionary.Add(0x5E, 0xBF	);  //# CYRILLIC CAPITAL LETTER E            
			convertJISToEucDictionary.Add(0x5F, 0xC0	);  //# CYRILLIC CAPITAL LETTER YU           
			convertJISToEucDictionary.Add(0x60, 0xC1	);  //# CYRILLIC CAPITAL LETTER YA           
			convertJISToEucDictionary.Add(0x70, 0xD1	);  //# CYRILLIC SMALL LETTER A              
			convertJISToEucDictionary.Add(0x71, 0xD2	);  //# CYRILLIC SMALL LETTER BE             
			convertJISToEucDictionary.Add(0x72, 0xD3	);  //# CYRILLIC SMALL LETTER VE             
			convertJISToEucDictionary.Add(0x73, 0xD4	);  //# CYRILLIC SMALL LETTER GHE            
			convertJISToEucDictionary.Add(0x74, 0xD5	);  //# CYRILLIC SMALL LETTER DE             
			convertJISToEucDictionary.Add(0x75, 0xD6	);  //# CYRILLIC SMALL LETTER IE             
			convertJISToEucDictionary.Add(0x76, 0xD7	);  //# CYRILLIC SMALL LETTER IO             
			convertJISToEucDictionary.Add(0x77, 0xD8	);  //# CYRILLIC SMALL LETTER ZHE            
			convertJISToEucDictionary.Add(0x78, 0xD9	);  //# CYRILLIC SMALL LETTER ZE             
			convertJISToEucDictionary.Add(0x79, 0xDA	);  //# CYRILLIC SMALL LETTER I              
			convertJISToEucDictionary.Add(0x7A, 0xDB	);  //# CYRILLIC SMALL LETTER SHORT I        
			convertJISToEucDictionary.Add(0x7B, 0xDC	);  //# CYRILLIC SMALL LETTER KA             
			convertJISToEucDictionary.Add(0x7C, 0xDD	);  //# CYRILLIC SMALL LETTER EL             
			convertJISToEucDictionary.Add(0x7D, 0xDE	);  //# CYRILLIC SMALL LETTER EM             
			convertJISToEucDictionary.Add(0x7E, 0xDF	);  //# CYRILLIC SMALL LETTER EN             
			convertJISToEucDictionary.Add(0x80, 0xE0	);  //# CYRILLIC SMALL LETTER O              
			convertJISToEucDictionary.Add(0x81, 0xE1	);  //# CYRILLIC SMALL LETTER PE             
			convertJISToEucDictionary.Add(0x82, 0xE2	);  //# CYRILLIC SMALL LETTER ER             
			convertJISToEucDictionary.Add(0x83, 0xE3	);  //# CYRILLIC SMALL LETTER ES             
			convertJISToEucDictionary.Add(0x84, 0xE4	);  //# CYRILLIC SMALL LETTER TE             
			convertJISToEucDictionary.Add(0x85, 0xE5	);  //# CYRILLIC SMALL LETTER U              
			convertJISToEucDictionary.Add(0x86, 0xE6	);  //# CYRILLIC SMALL LETTER EF             
			convertJISToEucDictionary.Add(0x87, 0xE7	);  //# CYRILLIC SMALL LETTER HA             
			convertJISToEucDictionary.Add(0x88, 0xE8	);  //# CYRILLIC SMALL LETTER TSE            
			convertJISToEucDictionary.Add(0x89, 0xE9	);  //# CYRILLIC SMALL LETTER CHE            
			convertJISToEucDictionary.Add(0x8A, 0xEA	);  //# CYRILLIC SMALL LETTER SHA            
			convertJISToEucDictionary.Add(0x8B, 0xEB	);  //# CYRILLIC SMALL LETTER SHCHA          
			convertJISToEucDictionary.Add(0x8C, 0xEC	);  //# CYRILLIC SMALL LETTER HARD SIGN      
			convertJISToEucDictionary.Add(0x8D, 0xED	);  //# CYRILLIC SMALL LETTER YERU           
			convertJISToEucDictionary.Add(0x8E, 0xEE	);  //# CYRILLIC SMALL LETTER SOFT SIGN      
			convertJISToEucDictionary.Add(0x8F, 0xEF	);  //# CYRILLIC SMALL LETTER E              
			convertJISToEucDictionary.Add(0x90, 0xF0	);  //# CYRILLIC SMALL LETTER YU             
			convertJISToEucDictionary.Add(0x91, 0xF1	);  //# CYRILLIC SMALL LETTER YA             
			
		}
		
		static Dictionary<int, int> convertJISToEucDictionary = new Dictionary<int, int>();		

		bool active;		
		public override int Read(byte[] buffer, int offset, int count)
		{
			int res = parent.Read(buffer, offset, count);
			int bytes_count = res;
			for(int i = offset; bytes_count > 0; bytes_count--,i++)
			{
				if(!active && buffer[i] == 0x84)
				{
					buffer[i] = 0xA7;
					active = true;
				}	
				else if(active)
				{
					buffer[i] = (byte)convertJISToEucDictionary[buffer[i]];
					active = false;
				}
			}
			return res;
		}
		
		
		public override int ReadByte()
		{
			return parent.ReadByte();
		}
		
		public override bool CanRead {
			get {
				return parent.CanRead;
			}
		}
		
		public override bool CanSeek {
			get {
				return parent.CanSeek;
			}
		}
		
		public override bool CanWrite {
			get {
				return parent.CanWrite;
			}
		}
		
		public override long Length {
			get {
				return parent.Length;
			}
		}
		
		public override long Position {
			get {
				return parent.Position;
			}
			set {
				parent.Position = value;
			}
		}
		
		public override void Flush()
		{
			parent.Flush();
		}
		
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}
		
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}
		
		
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}
	
	}
}
