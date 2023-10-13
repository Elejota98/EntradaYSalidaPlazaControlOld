using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MC.ModuloEntrada.WinForm.FrontEnd
{
    [XmlRoot(ElementName = "Plate", Namespace = "http://www.hikvision.com/ver20/XMLSchema")]
    public class Plate
    {
        [XmlElement(ElementName = "captureTime", Namespace = "http://www.hikvision.com/ver20/XMLSchema")]
        public string CaptureTime { get; set; }
        [XmlElement(ElementName = "plateNumber", Namespace = "http://www.hikvision.com/ver20/XMLSchema")]
        public string PlateNumber { get; set; }
        [XmlElement(ElementName = "picName", Namespace = "http://www.hikvision.com/ver20/XMLSchema")]
        public string PicName { get; set; }
        [XmlElement(ElementName = "country", Namespace = "http://www.hikvision.com/ver20/XMLSchema")]
        public string Country { get; set; }
        [XmlElement(ElementName = "laneNo", Namespace = "http://www.hikvision.com/ver20/XMLSchema")]
        public string LaneNo { get; set; }
        [XmlElement(ElementName = "direction", Namespace = "http://www.hikvision.com/ver20/XMLSchema")]
        public string Direction { get; set; }
        [XmlElement(ElementName = "matchingResult", Namespace = "http://www.hikvision.com/ver20/XMLSchema")]
        public string MatchingResult { get; set; }
    }

    [XmlRoot(ElementName = "Plates", Namespace = "http://www.hikvision.com/ver20/XMLSchema")]
    public class Plates
    {
        [XmlElement(ElementName = "Plate", Namespace = "http://www.hikvision.com/ver20/XMLSchema")]
        public List<Plate> Plate { get; set; }
        [XmlAttribute(AttributeName = "version")]
        public string Version { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

}
