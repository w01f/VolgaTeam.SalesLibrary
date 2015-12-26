using System;
using System.Security.Cryptography;
using System.Text;

namespace SalesLibraries.SiteManager.ToolClasses
{
	public class PasswordGenerator
	{
		public PasswordGenerator()
		{
			Minimum = DefaultMinimum;
			Maximum = DefaultMaximum;
			ConsecutiveCharacters = false;
			RepeatCharacters = true;
			ExcludeSymbols = false;
			Exclusions = null;

			rng = new RNGCryptoServiceProvider();
		}

		protected int GetCryptographicRandomNumber(int lBound, int uBound)
		{
			// Assumes lBound >= 0 && lBound < uBound
			// returns an int >= lBound and < uBound
			uint urndnum;
			byte[] rndnum = new Byte[4];
			if (lBound == uBound - 1)
			{
				// test for degenerate case where only lBound can be returned   
				return lBound;
			}

			uint xcludeRndBase = (uint.MaxValue - (uint.MaxValue % (uint)(uBound - lBound)));

			do
			{
				rng.GetBytes(rndnum);
				urndnum = BitConverter.ToUInt32(rndnum, 0);
			} while (urndnum >= xcludeRndBase);

			return (int)(urndnum % (uBound - lBound)) + lBound;
		}

		protected char GetRandomCharacter()
		{
			int upperBound = pwdCharArray.GetUpperBound(0);

			if (true == ExcludeSymbols)
			{
				upperBound = UBoundDigit;
			}

			int randomCharPosition = GetCryptographicRandomNumber(pwdCharArray.GetLowerBound(0), upperBound);

			char randomChar = pwdCharArray[randomCharPosition];

			return randomChar;
		}

		protected char GetRandomDigit()
		{
			return pwdDigitArray[GetCryptographicRandomNumber(pwdDigitArray.GetLowerBound(0), pwdDigitArray.GetUpperBound(0))];
		}

		protected bool InserDigit()
		{
			return new Random().Next() % 2 == 0 ? true : false;
		}

		public string Generate()
		{
			// Pick random length between minimum and maximum   
			int pwdLength = GetCryptographicRandomNumber(Minimum, Maximum);

			StringBuilder pwdBuffer = new StringBuilder();
			pwdBuffer.Capacity = Maximum;

			// Generate random characters
			char lastCharacter, nextCharacter;

			// Initial dummy character flag
			lastCharacter = nextCharacter = '\n';

			bool digitIncluded = false;

			for (int i = 0; i < pwdLength; i++)
			{
				if ((!digitIncluded && InserDigit()) || (!digitIncluded && i == (pwdLength - 1)))
				{
					nextCharacter = GetRandomDigit();
					digitIncluded = true;
				}
				else
					nextCharacter = GetRandomCharacter();


				if (false == ConsecutiveCharacters)
				{
					while (lastCharacter == nextCharacter)
					{
						if ((!digitIncluded && InserDigit()) || (!digitIncluded && i == (pwdLength - 1)))
						{
							nextCharacter = GetRandomDigit();
							digitIncluded = true;
						}
						else
							nextCharacter = GetRandomCharacter();
					}
				}

				if (false == RepeatCharacters)
				{
					string temp = pwdBuffer.ToString();
					int duplicateIndex = temp.IndexOf(nextCharacter);
					while (-1 != duplicateIndex)
					{
						if ((!digitIncluded && InserDigit()) || (!digitIncluded && i == (pwdLength - 1)))
						{
							nextCharacter = GetRandomDigit();
							digitIncluded = true;
						}
						else
							nextCharacter = GetRandomCharacter();
						duplicateIndex = temp.IndexOf(nextCharacter);
					}
				}

				pwdBuffer.Append(nextCharacter);
				lastCharacter = nextCharacter;
			}

			if (null != pwdBuffer)
			{
				return pwdBuffer.ToString();
			}
			else
			{
				return String.Empty;
			}
		}

		public string Exclusions
		{
			get { return exclusionSet; }
			set { exclusionSet = value; }
		}

		public int Minimum
		{
			get { return minSize; }
			set
			{
				minSize = value;
				if (DefaultMinimum > minSize)
				{
					minSize = DefaultMinimum;
				}
			}
		}

		public int Maximum
		{
			get { return maxSize; }
			set
			{
				maxSize = value;
				if (minSize >= maxSize)
				{
					maxSize = DefaultMaximum;
				}
			}
		}

		public bool ExcludeSymbols
		{
			get { return hasSymbols; }
			set { hasSymbols = value; }
		}

		public bool RepeatCharacters
		{
			get { return hasRepeating; }
			set { hasRepeating = value; }
		}

		public bool ConsecutiveCharacters
		{
			get { return hasConsecutive; }
			set { hasConsecutive = value; }
		}

		private const int DefaultMinimum = 4;
		private const int DefaultMaximum = 5;
		private const int UBoundDigit = 61;

		private RNGCryptoServiceProvider rng;
		private int minSize;
		private int maxSize;
		private bool hasRepeating;
		private bool hasConsecutive;
		private bool hasSymbols;
		private string exclusionSet;
		private char[] pwdCharArray = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
		private char[] pwdDigitArray = "0123456789".ToCharArray();
	}
}
