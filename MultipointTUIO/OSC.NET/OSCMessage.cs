using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace OSC.NET
{
	/// <summary>
	/// OSCMessage
    /// 
    /// 
    /*  <OSCPACKET ADDRESS="127.0.0.1" PORT="49178" TIME="1">
          <MESSAGE NAME="/tuio/2Dcur">
            <ARGUMENT TYPE="s" VALUE="set"/>
            <ARGUMENT TYPE="i" VALUE="0"/>
            <ARGUMENT TYPE="f" VALUE="0.7452997"/>
            <ARGUMENT TYPE="f" VALUE="0.6298934"/>
            <ARGUMENT TYPE="f" VALUE="0.0"/>
            <ARGUMENT TYPE="f" VALUE="0.0"/>
            <ARGUMENT TYPE="f" VALUE="0.0"/>
            <ARGUMENT TYPE="f" VALUE="0.49701047"/>
            <ARGUMENT TYPE="f" VALUE="-0.13280672"/>
          </MESSAGE>
     * 
     *  <OSCPACKET ADDRESS="127.0.0.1" PORT="50387" TIME="0">
     *      <MESSAGE NAME="/tuio/2Dcur">
     *          <ARGUMENT TYPE="s" VALUE="set" />
     *          <ARGUMENT TYPE="i" VALUE="0" />
     *          <ARGUMENT TYPE="f" VALUE="0.395" />
     *          <ARGUMENT TYPE="f" VALUE="0.6366667" />
     *          <ARGUMENT TYPE="f" VALUE="0" />
     *          <ARGUMENT TYPE="f" VALUE="0" />
     *          <ARGUMENT TYPE="f" VALUE="0" />
     *       </MESSAGE>
     *   </OSCPACKET><OSCPACKET ADDRESS="127.0.0.1" PORT="50387" TIME="0"><MESSAGE NAME="/tuio/2Dcur"><ARGUMENT TYPE="s" VALUE="alive" /><ARGUMENT TYPE="i" VALUE="0" /></MESSAGE></OSCPACKET>
     * 
     * 
     * 
     * 
          <MESSAGE NAME="/tuio/2Dcur">
            <ARGUMENT TYPE="s" VALUE="alive"/>
            <ARGUMENT TYPE="i" VALUE="0"/>
            <ARGUMENT TYPE="i" VALUE="17"/>
            <ARGUMENT TYPE="i" VALUE="43"/>
            <ARGUMENT TYPE="i" VALUE="48"/>
            <ARGUMENT TYPE="i" VALUE="53"/>
          </MESSAGE>
          <MESSAGE NAME="/tuio/2Dcur">
            <ARGUMENT TYPE="s" VALUE="fseq"/>
            <ARGUMENT TYPE="i" VALUE="0"/>
          </MESSAGE>
        </OSCPACKET> */
    /// </summary>
    
	public class OSCMessage : OSCPacket
	{
		protected const char INTEGER = 'i';
		protected const char FLOAT	  = 'f';
		protected const char LONG	  = 'h';
		protected const char DOUBLE  = 'd';
		protected const char STRING  = 's';
		protected const char SYMBOL  = 'S';
		//protected const char BLOB	  = 'b';
		//protected const char ALL     = '*';

        protected XmlDocument xdoc;

		public OSCMessage(string address)
		{
			this.typeTag = ",";
			this.Address = address;

            // Prepare XML message 
            xdoc = new XmlDocument();
            
            XmlElement message = xdoc.CreateElement("MESSAGE");
            XmlAttribute name = xdoc.CreateAttribute("NAME");
            name.Value = address;

            message.Attributes.Append(name);
            xdoc.AppendChild(message);
		}

		public OSCMessage(string address, object value)
		{
			this.typeTag = ",";
			this.Address = address;
			Append(value);
		}

		override protected void pack()
		{
			ArrayList data = new ArrayList();

			addBytes(data, packString(this.address));
			padNull(data);
			addBytes(data, packString(this.typeTag));
			padNull(data);
			
			foreach(object value in this.Values)
			{
				if(value is int) addBytes(data, packInt((int)value));
				else if(value is long) addBytes(data, packLong((long)value));
				else if(value is float) addBytes(data, packFloat((float)value));
				else if(value is double) addBytes(data, packDouble((double)value));
				else if(value is string)
				{
					addBytes(data, packString((string)value));
					padNull(data);
				}
				else 
				{
					// TODO
				}
			}
			
			this.binaryData = (byte[])data.ToArray(typeof(byte));
		}


		public static OSCMessage Unpack(byte[] bytes, ref int start)
		{
			string address = unpackString(bytes, ref start);
			//Console.WriteLine("address: " + address);
			OSCMessage msg = new OSCMessage(address);

			char[] tags = unpackString(bytes, ref start).ToCharArray();
			//Console.WriteLine("tags: " + new string(tags));
			foreach(char tag in tags)
			{
				//Console.WriteLine("tag: " + tag + " @ "+start);
				if(tag == ',') continue;
				else if(tag == INTEGER) msg.Append(unpackInt(bytes, ref start));
				else if(tag == LONG) msg.Append(unpackLong(bytes, ref start));
				else if(tag == DOUBLE) msg.Append(unpackDouble(bytes, ref start));
				else if(tag == FLOAT) msg.Append(unpackFloat(bytes, ref start));
				else if(tag == STRING || tag == SYMBOL) msg.Append(unpackString(bytes, ref start));
				else Console.WriteLine("unknown tag: "+tag);
			}

			return msg;
		}

		override public void Append(object value)
		{
            XmlElement argument = xdoc.CreateElement("ARGUMENT");
            XmlAttribute type = xdoc.CreateAttribute("TYPE");
            
			if(value is int)
			{
				AppendTag(INTEGER);
                type.Value = "i";
			}
			else if(value is long)
			{
				AppendTag(LONG);
                type.Value = "l";
			}
			else if(value is float)
			{
				AppendTag(FLOAT);
                type.Value = "f";
			}
			else if(value is double)
			{
				AppendTag(DOUBLE);
                type.Value = "d";
			}
			else if(value is string)
			{
				AppendTag(STRING);
                type.Value = "s";
			}
			else 
			{
				// TODO: exception
			}
			values.Add(value);

            argument.Attributes.Append(type);
            XmlAttribute valueAttr = xdoc.CreateAttribute("VALUE");

            if (value is float)
                valueAttr.Value = String.Format("{0:0.00000000}", value);
            else
                valueAttr.Value = value.ToString();
            
            argument.Attributes.Append(valueAttr);
            xdoc.DocumentElement.AppendChild(argument);
		}

		protected string typeTag;
       

		protected void AppendTag(char type)
		{
			typeTag += type;
		}

        public XmlElement ToXml { get { return xdoc.DocumentElement; } }

		override public bool IsBundle() { return false; }
	}
}
