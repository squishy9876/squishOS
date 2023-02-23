using System;
using ComputerInterface.Interfaces;

namespace squishOS.ComputerInterface
{
    public class MapBrowseEntry : IComputerModEntry
    {

        public string EntryName
        {
            get
            {
                return "squishOS";
            }
        }

        public Type EntryViewType
        {
            get
            {
                return typeof(MapBrowseView);
            }
        }
    }