using System.Reflection;
using System.Xml.Linq;
using Y.Mold.Parse.Services;

namespace Y.Mold.Parse;

   /// <summary>
    /// Molding parser class.
    /// </summary>
    public class MoldingParser : IMoldingParser
    {
        [PositionAttribute]
        [MustBePresentAttribute]
        private (string name, EPositionType molding) X1Name => ("x1", EPositionType.X1);

        [PositionAttribute]
        [MustBePresentAttribute]
        private (string name, EPositionType molding) X2Name => ("x2", EPositionType.X2);

        [PositionAttribute]
        [MustBePresentAttribute]
        private (string name, EPositionType molding) Y1Name => ("y1", EPositionType.Y1);

        [PositionAttribute]
        [MustBePresentAttribute]
        private (string name, EPositionType molding) YName => ("y2", EPositionType.Y2);

        [PositionAttribute]
        private (string name, EPositionType molding) HeightStartName => ("height_start", EPositionType.HeightStart);

        [PositionAttribute]
        private (string name, EPositionType molding) HeightEndName => ("height_end", EPositionType.HeightEnd);

        [PositionAttribute]
        private (string name, EPositionType molding) HeightAvgName => ("height_avg", EPositionType.HeightAvg);

        /// <inheritdoc/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "Local functions.")]
        public List<MoldingTag> Parse(string xml)
        {
            List<MoldingTag> rlist = new List<MoldingTag>();

            T ThrowExceptionIfNullOrGetDefault<T>(object value, string id, bool throwing)
            {
                if (value != null)
                {
                    return (T)value;
                }

                if (throwing)
                {
                    throw new MissingFieldException(id);
                }

                return (T)default;
            }

            double? ParseDouble(string val)
            {
                if (val == null)
                {
                    return null;
                }

                if (double.TryParse(val, out double res))
                {
                    return res;
                }

                return null;
            }

            var doc = XDocument.Parse(xml);
            var elements = doc.Root?.Elements();

            if (elements != null)
            {
                foreach (var element in elements)
                {
                    if (element.Name.LocalName.Equals("line", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var id = ThrowExceptionIfNullOrGetDefault<string>(element.Attribute("id")?.Value, "id", true);

                        var posValues = new List<(EPositionType moldingtype, double? value)>();

                        foreach (var p in typeof(MoldingParser).GetProperties(BindingFlags.NonPublic | BindingFlags.Public
                            | BindingFlags.Instance | BindingFlags.Static))
                        {
                            if (Attribute.IsDefined(p, typeof(PositionAttribute)))
                            {
                                var propValue = ((string name, EPositionType moldingtype))this.GetPropValue(p.Name);
                                var v = ParseDouble(element.Attribute(propValue.name)?.Value);
                                var f = ThrowExceptionIfNullOrGetDefault<double>(
                                    v,
                                    propValue.name,
                                    Attribute.IsDefined(p, typeof(MustBePresentAttribute)));
                                posValues.Add((propValue.moldingtype, f));
                            }
                        }

                        var txt = element.Attribute("text")?.Value;
                        var isTxt = txt != null;
                        var stroke = element.Attribute("stroke")?.Value;

                        var vectorPath = element.Attribute("vector")?.Value;

                        double GetPositionByEnum(EPositionType type)
                        {
                            var value = posValues.FirstOrDefault(x => x.moldingtype.Equals(type)).value;
                            return value ?? default;
                        }

                        rlist.Add(
                            new MoldingTag(
                                id,
                                GetPositionByEnum(EPositionType.X1),
                                GetPositionByEnum(EPositionType.Y1),
                                GetPositionByEnum(EPositionType.X2),
                                GetPositionByEnum(EPositionType.Y2),
                                txt,
                                isTxt,
                                stroke,
                                GetPositionByEnum(EPositionType.HeightStart),
                                GetPositionByEnum(EPositionType.HeightEnd),
                                GetPositionByEnum(EPositionType.HeightAvg),
                                vectorPath));
                    }
                }
            }

            return rlist;
        }
    }