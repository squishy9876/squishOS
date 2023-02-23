using ComputerInterface;
using ComputerInterface.ViewLib;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using UnityEngine;
using squishOS.Helpers;

namespace squishOS.ComputerInterface
{
    public class ModBrowseView : ComputerView
    {
        private const int PageSize = 9;
        private readonly UIElementPageHandler<Mod> _pageHandler;
        private readonly UISelectionHandler _selectionHandler;
        private readonly HttpClient _client;

        private static List<Mod> _modList = new List<Mod>();
        private Mod _selectedMod;

        private bool _isError;

        private ModRoot _modResponse;

        private ModBrowseOptions _options = new MapBrowseOptions();

        private MapBrowseView(HttpClient client)
        {
            _selectionHandler = new UISelectionHandler(EKeyboardKey.Up, EKeyboardKey.Down, EKeyboardKey.Enter);
            _selectionHandler.OnSelected += OnModSelected;
        }
    }
}

public override async void OnShow(object[] args)
{
    base.OnShow(args);

    bool forceRefresh = false;
    if (args.Length > 0)
    {
        if (args[0] is bool b)
        {
            forceRefresh = b;
        }
    }

    if (_modResponse == null || forceRefresh)
    {
        SetText(str =>
        {
            str.Repeat("=", SCREEN_WIDTH).AppendLine();
            str.BeginCenter().Append("squishOS").AppendLine();
            str.Repeat("=", SCREEN_WIDTH);
            str.Append("by squishy#9555");
            str.Repeat("=", SCREEN_WIDTH);
            var str = new StringBuilder();
            // 
            str.AppendClr("[^ | v] SELECT MOD   |   [ENTER] INJECT/UNINJECT").AppendLine();
        });

        private void DrawNoMods(StringBuilder str)
        {
            // TODO: FINISH CHECKING DIRECTORY
            str.Append("NO MODS FOUND.").Repeat("\n", 2);
            str.Append("If you have mods in the folder").AppendLine();
            str.Append(" make sure they are in the right format, .dll").Repeat("\n", 2);
            str.BeginCenter().Append("PRESS ANY BUTTON TO CONTINUE...").EndAlign();
        }