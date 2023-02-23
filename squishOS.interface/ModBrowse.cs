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

        private ModBrowseOptions _options = new modBrowseOptions();

        private modBrowseView(HttpClient client)
        {
            _selectionHandler = new UISelectionHandler(EKeyboardKey.Up, EKeyboardKey.Down, EKeyboardKey.Enter);
            _selectionHandler.OnSelected += OnModSelected;
        }
    }
}

// damn i hope this doesnt break
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
            str.AppendClr("[^ | v] SELECT MOD   |   [ENTER] INJECT/UNINJECT").AppendLine();
        });

        private void DrawNoMods(StringBuilder str)
        {
            str.Append("NO MODS FOUND.").Repeat("\n", 2);
            str.Append("If you have mods in the folder").AppendLine();
            str.Append(" make sure they are in the right format, .dll").Repeat("\n", 2);
            str.BeginCenter().Append("PRESS ANY BUTTON TO CONTINUE...").EndAlign();
        }

        private void OnModSelected(int _)
        {
            if (_selectedMod == null) return;

            ShowView<ModBrowseDetailsView>(_selectedmod);
        }


         public override async void OnKeyPressed(EKeyboardKey key)
        {
    if (_isError)
    {
        _isError = false;
        ReturnToMainMenu();
        return;
    }

    if (_selectionHandler.HandleKeypress(key))
    {
        var selectedIdx = _pageHandler.GetAbsoluteIndex(_selectionHandler.CurrentSelectionIndex);
        if (_modList[selectedIdx] == null) _selectionHandler.MoveSelectionUp();
        DrawList();
        return;
    }

    if (_pageHandler.HandleKeyPress(key))
    {
        _selectionHandler.CurrentSelectionIndex = 0;

        if (_pageHandler.CurrentPage == _pageHandler.MaxPage)
        {
            if (_modResponse.Data.Count > _modList.Count)
            {
                int currentPage = _pageHandler.CurrentPage;
                _modResponse = await GetModsAsync(PageSize, _pageHandler.CurrentPage);
                _modList.RemoveAt(_modList.Count - 1);
                _modList.AddRange(_modResponse.Data.mods);
                _modList.Add(null);
                _pageHandler.SetElements(_modList.ToArray());
                _pageHandler.CurrentPage = currentPage;
            }
        }

        DrawList();
        return;
         }
    }
    
       switch (key)
{
    case EKeyboardKey.Option1:
        ShowView<MapBrowseOptionsView>(_options);
        break;
    case EKeyboardKey.Back:
        PreviewOrb.HideOrb();
        ReturnToMainMenu();
        break;
}
		}
	}
}
