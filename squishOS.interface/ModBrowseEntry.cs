using System;
using ComputerInterface.Interfaces;

// name that shows up on computer interface
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