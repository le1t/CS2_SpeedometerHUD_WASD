using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Core.Capabilities;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Cvars;
using CounterStrikeSharp.API.Modules.Utils;
using System.Text.Json.Serialization;
using System.Collections.Concurrent;
using System.Drawing;
using CS2_GameHUDAPI;
using System.Globalization;

namespace CS2_SpeedometerHUD_WASD;

public class SpeedometerConfig : BasePluginConfig
{
    /// <summary>
    /// Включить/выключить плагин SpeedometerHUD+WASD (по умолчанию: true)
    /// true - включено, false - выключено
    /// </summary>
    [JsonPropertyName("css_speedometer_enabled")]
    public bool Enabled { get; set; } = true;
    
    /// <summary>
    /// Канал HUD для отображения (0-255) (по умолчанию: 1)
    /// Диапазон: 0-255
    /// </summary>
    [JsonPropertyName("css_speedometer_hud_channel")]
    public int HudChannel { get; set; } = 1;
    
    /// <summary>
    /// Позиция HUD по оси X (по умолчанию: -2.3)
    /// Диапазон: -100.0 до 100.0
    /// </summary>
    [JsonPropertyName("css_speedometer_hud_x")]
    public float HudX { get; set; } = -2.3f;
    
    /// <summary>
    /// Позиция HUD по оси Y (по умолчанию: -3.5)
    /// Диапазон: -100.0 до 100.0
    /// </summary>
    [JsonPropertyName("css_speedometer_hud_y")]
    public float HudY { get; set; } = -3.5f;
    
    /// <summary>
    /// Позиция HUD по оси Z (высота) (по умолчанию: 6.7)
    /// Диапазон: 0.0 до 20.0
    /// </summary>
    [JsonPropertyName("css_speedometer_hud_z")]
    public float HudZ { get; set; } = 6.7f;
    
    /// <summary>
    /// Размер шрифта для HUD (по умолчанию: 40)
    /// Диапазон: 10-100
    /// </summary>
    [JsonPropertyName("css_speedometer_font_size")]
    public int FontSize { get; set; } = 40;
    
    /// <summary>
    /// Имя шрифта для HUD (по умолчанию: "Consolas")
    /// </summary>
    [JsonPropertyName("css_speedometer_font_name")]
    public string FontName { get; set; } = "Consolas";
    
    /// <summary>
    /// Единиц на пиксель для шрифта (по умолчанию: 0.0057)
    /// Диапазон: 0.001 до 1.0
    /// </summary>
    [JsonPropertyName("css_speedometer_units_per_px")]
    public float UnitsPerPixel { get; set; } = 0.0057f;
    
    /// <summary>
    /// Цвет HUD в формате R,G,B (по умолчанию: "255,255,255" - белый)
    /// Диапазон каждой компоненты: 0-255
    /// </summary>
    [JsonPropertyName("css_speedometer_color")]
    public string Color { get; set; } = "255,255,255";
    
    /// <summary>
    /// Интервал обновления скорости (секунды) (по умолчанию: 0.01)
    /// Диапазон: 0.01 до 1.0
    /// </summary>
    [JsonPropertyName("css_speedometer_update_interval")]
    public float UpdateInterval { get; set; } = 0.01f;
    
    /// <summary>
    /// Показывать нажатые клавиши движения (по умолчанию: true)
    /// true - показывать, false - скрыть
    /// </summary>
    [JsonPropertyName("css_speedometer_show_keys")]
    public bool ShowKeys { get; set; } = true;
    
    /// <summary>
    /// Показывать HUD в режиме наблюдателя и после смерти (по умолчанию: true)
    /// true - показывать, false - скрыть
    /// </summary>
    [JsonPropertyName("css_speedometer_show_spectator")]
    public bool ShowSpectator { get; set; } = true;
    
    /// <summary>
    /// Уровень логирования плагина (по умолчанию: 0)
    /// 0 - только ошибки
    /// 1 - ошибки и информация
    /// 2 - ошибки, информация и отладка
    /// Диапазон: 0-2
    /// </summary>
    [JsonPropertyName("css_speedometer_log_level")]
    public int LogLevel { get; set; } = 0;
    
    /// <summary>
    /// Толщина обводки текста (0.0 = нет обводки) (по умолчанию: 0.0)
    /// Диапазон: 0.0 до 10.0
    /// </summary>
    [JsonPropertyName("css_speedometer_text_border_width")]
    public float TextBorderWidth { get; set; } = 0.0f;
    
    /// <summary>
    /// Высота обводки текста (0.0 = нет обводки) (по умолчанию: 0.0)
    /// Диапазон: 0.0 до 10.0
    /// </summary>
    [JsonPropertyName("css_speedometer_text_border_height")]
    public float TextBorderHeight { get; set; } = 0.0f;
    
    /// <summary>
    /// Использовать жирный шрифт (по умолчанию: true)
    /// true - жирный, false - обычный
    /// </summary>
    [JsonPropertyName("css_speedometer_use_bold_font")]
    public bool UseBoldFont { get; set; } = true;
    
    /// <summary>
    /// Метод отображения HUD (по умолчанию: 1)
    /// 1 - point_orient (стабильный, без тряски)
    /// 0 - телепорт (быстрый, но может трястись)
    /// </summary>
    [JsonPropertyName("css_speedometer_hud_method")]
    public int HudMethod { get; set; } = 1;
    
    /// <summary>
    /// Время отображения JU после прыжка (секунды) (по умолчанию: 0.4)
    /// Диапазон: 0.1 до 1.0
    /// </summary>
    [JsonPropertyName("css_speedometer_jump_display_time")]
    public float JumpDisplayTime { get; set; } = 0.4f;
    
    /// <summary>
    /// Следующий прыжок в течение N секунд считается распрыжкой (по умолчанию: 0.5)
    /// Диапазон: 0.1 до 1.0
    /// </summary>
    [JsonPropertyName("css_speedometer_bunnyhop_window")]
    public float BunnyhopWindow { get; set; } = 0.5f;
}

[MinimumApiVersion(362)]
public class CS2_SpeedometerHUD_WASD : BasePlugin, IPluginConfig<SpeedometerConfig>
{
    public override string ModuleName => "CS2 SpeedometerHUD+WASD";
    public override string ModuleVersion => "1.9";
    public override string ModuleAuthor => "Fixed by le1t1337 + AI DeepSeek. Code logic by phantom, Cruze";
    
    public required SpeedometerConfig Config { get; set; }
    
    private static IGameHUDAPI? _hudapi;
    
    private readonly ConcurrentDictionary<int, UserSettings> _userSettings = new();
    private CounterStrikeSharp.API.Modules.Timers.Timer? _updateTimer;
    private Color _hudColor = Color.White;
    private readonly ConcurrentDictionary<int, bool> _hudInitialized = new();
    private readonly ConcurrentDictionary<int, bool> _playerActive = new();
    
    // Для отслеживания прыжков
    private readonly ConcurrentDictionary<int, bool> _previousJumpButton = new();
    private readonly ConcurrentDictionary<int, DateTime> _lastJumpPress = new();
    private readonly ConcurrentDictionary<int, DateTime> _lastJumpEvent = new();
    private readonly ConcurrentDictionary<int, bool> _wasOnGround = new();
    
    // Константы для клавиш
    private const ulong INSPECT_BUTTON = 34359738368ul;
    private const ulong SCORE_BUTTON = 8589934592ul;
    
    public void OnConfigParsed(SpeedometerConfig config)
    {
        Config = config;
        
        Config.HudChannel = Math.Clamp(Config.HudChannel, 0, 255);
        Config.HudX = Math.Clamp(Config.HudX, -100.0f, 100.0f);
        Config.HudY = Math.Clamp(Config.HudY, -100.0f, 100.0f);
        Config.HudZ = Math.Clamp(Config.HudZ, 0.0f, 20.0f);
        Config.FontSize = Math.Clamp(Config.FontSize, 10, 100);
        Config.UnitsPerPixel = Math.Clamp(Config.UnitsPerPixel, 0.001f, 1.0f);
        Config.UpdateInterval = Math.Clamp(Config.UpdateInterval, 0.01f, 1.0f);
        Config.LogLevel = Math.Clamp(Config.LogLevel, 0, 2);
        Config.TextBorderWidth = Math.Clamp(Config.TextBorderWidth, 0.0f, 10.0f);
        Config.TextBorderHeight = Math.Clamp(Config.TextBorderHeight, 0.0f, 10.0f);
        Config.HudMethod = Math.Clamp(Config.HudMethod, 0, 1);
        Config.JumpDisplayTime = Math.Clamp(Config.JumpDisplayTime, 0.1f, 1.0f);
        Config.BunnyhopWindow = Math.Clamp(Config.BunnyhopWindow, 0.1f, 1.0f);
        
        try
        {
            var colorParts = Config.Color.Split(',');
            if (colorParts.Length == 3)
            {
                _hudColor = Color.FromArgb(
                    int.Parse(colorParts[0]),
                    int.Parse(colorParts[1]),
                    int.Parse(colorParts[2])
                );
            }
        }
        catch
        {
            _hudColor = Color.White;
            LogError("Ошибка при парсинге цвета, используется белый по умолчанию");
        }
        
        LogInfo($"Конфиг загружен: Включен={Config.Enabled}, Канал HUD={Config.HudChannel}, Метод HUD={Config.HudMethod} ({(Config.HudMethod == 1 ? "point_orient" : "teleport")})");
    }
    
    public override void Load(bool hotReload)
    {
        // Устанавливаем метод HUD перед инициализацией
        SetGameHUDMethod(Config.HudMethod == 1);
        
        // Основные команды
        AddCommand("css_speedometer_help", "Показать справку по плагину SpeedometerHUD+WASD", OnHelpCommand);
        AddCommand("css_speedometer_settings", "Показать текущие настройки SpeedometerHUD+WASD", OnSettingsCommand);
        AddCommand("css_speedometer_reload", "Перезагрузить конфигурацию SpeedometerHUD+WASD", OnReloadCommand);
        AddCommand("css_speedometer_toggle", "Включить/выключить отображение спидометра для себя", OnToggleCommand);
        AddCommand("css_speedometer_test", "Тестовая команда для проверки работы плагина", OnTestCommand);
        AddCommand("css_speedometer_enable", "Включить плагин SpeedometerHUD+WASD (по умолчанию: включено)", OnEnableCommand);
        AddCommand("css_speedometer_disable", "Выключить плагин SpeedometerHUD+WASD", OnDisableCommand);
        
        // Команды для всех переменных конфига
        AddCommand("css_speedometer_setenabled", "Включить/выключить плагин (0/1) (по умолчанию: 1)", OnSetEnabledCommand);
        AddCommand("css_speedometer_setchannel", "Установить канал HUD (0-255) (по умолчанию: 1)", OnSetChannelCommand);
        AddCommand("css_speedometer_setposition", "Установить позицию HUD (X Y Z) (по умолчанию: -2.3 -3.5 6.7)", OnSetPositionCommand);
        AddCommand("css_speedometer_setcolor", "Установить цвет HUD (R,G,B) (по умолчанию: 255,255,255)", OnSetColorCommand);
        AddCommand("css_speedometer_setfontsize", "Установить размер шрифта (10-100) (по умолчанию: 40)", OnSetFontSizeCommand);
        AddCommand("css_speedometer_setfontname", "Установить имя шрифта (по умолчанию: Consolas)", OnSetFontNameCommand);
        AddCommand("css_speedometer_setunits", "Установить единиц на пиксель (0.001-1.0) (по умолчанию: 0.0057)", OnSetUnitsCommand);
        AddCommand("css_speedometer_setinterval", "Установить интервал обновления (0.01-1.0) (по умолчанию: 0.01)", OnSetIntervalCommand);
        AddCommand("css_speedometer_setkeys", "Показывать нажатые клавиши движения (0/1) (по умолчанию: 1)", OnSetKeysCommand);
        AddCommand("css_speedometer_setspectator", "Показывать HUD в режиме наблюдателя (0/1) (по умолчанию: 1)", OnSetSpectatorCommand);
        AddCommand("css_speedometer_setloglevel", "Установить уровень логирования (0-2) (по умолчанию: 0)", OnSetLogLevelCommand);
        AddCommand("css_speedometer_setborder", "Установить обводку текста (ширина высота) (по умолчанию: 0.0 0.0)", OnSetBorderCommand);
        AddCommand("css_speedometer_setbold", "Использовать жирный шрифт (0/1) (по умолчанию: 1)", OnSetBoldCommand);
        AddCommand("css_speedometer_setmethod", "Установить метод HUD (0-телепорт, 1-point_orient) (по умолчанию: 1)", OnSetMethodCommand);
        AddCommand("css_speedometer_setjumptime", "Установить время отображения прыжка (0.1-1.0) (по умолчанию: 0.4)", OnSetJumpTimeCommand);
        AddCommand("css_speedometer_setbhopwindow", "Установить окно распрыжки (0.1-1.0) (по умолчанию: 0.5)", OnSetBhopWindowCommand);
        
        // Псевдонимы команд для совместимости
        AddCommand("css_speedometer_togglekeys", "Включить/выключить отображение клавиш (по умолчанию: включено)", OnToggleKeysCommand);
        AddCommand("css_speedometer_togglespectator", "Включить/выключить отображение в режиме наблюдателя (по умолчанию: включено)", OnToggleSpectatorCommand);
        AddCommand("css_speedometer_togglebold", "Включить/выключить жирный шрифт (по умолчанию: включено)", OnToggleBoldCommand);
        
        RegisterEventHandler<EventPlayerConnectFull>(OnPlayerConnectFull);
        RegisterEventHandler<EventPlayerDisconnect>(OnPlayerDisconnect);
        RegisterEventHandler<EventRoundStart>(OnRoundStart);
        RegisterEventHandler<EventPlayerSpawn>(OnPlayerSpawn);
        RegisterEventHandler<EventPlayerDeath>(OnPlayerDeath);
        RegisterEventHandler<EventPlayerTeam>(OnPlayerTeam);
        RegisterEventHandler<EventPlayerJump>(OnPlayerJump);
        
        if (hotReload)
        {
            Server.NextFrame(() =>
            {
                foreach (var player in Utilities.GetPlayers())
                {
                    if (player != null && player.IsValid && !player.IsBot && !player.IsHLTV)
                    {
                        InitializePlayer(player);
                    }
                }
            });
        }
        
        PrintConVarInfo();
        LogInfo($"Плагин v{ModuleVersion} успешно загружен");
    }
    
    public override void OnAllPluginsLoaded(bool hotReload)
    {
        try
        {
            PluginCapability<IGameHUDAPI> capability = new("gamehud:api");
            _hudapi = IGameHUDAPI.Capability.Get();
            LogInfo("GameHUD API загружена успешно");
        }
        catch (Exception ex)
        {
            _hudapi = null;
            LogError($"Ошибка загрузки GameHUD API: {ex.Message}");
            LogError("Спидометр HUD не будет работать без GameHUD API");
        }
    }
    
    private void SetGameHUDMethod(bool useOrientMethod)
    {
        try
        {
            var methodConVar = ConVar.Find("css_gamehud_method");
            if (methodConVar != null)
            {
                methodConVar.SetValue(useOrientMethod);
                LogInfo($"Установлен метод GameHUD: {(useOrientMethod ? "point_orient (стабильный)" : "teleport (быстрый)")}");
            }
            else
            {
                LogWarning("Не удалось найти консольную переменную css_gamehud_method. Убедитесь, что плагин GameHUD загружен.");
            }
        }
        catch (Exception ex)
        {
            LogError($"Ошибка установки метода GameHUD: {ex.Message}");
        }
    }
    
    private void PrintConVarInfo()
    {
        Console.WriteLine("===============================================");
        Console.WriteLine($"[SpeedometerHUD+WASD] Плагин успешно загружен!");
        Console.WriteLine($"[SpeedometerHUD+WASD] Версия: {ModuleVersion}");
        Console.WriteLine("[SpeedometerHUD+WASD] Важные настройки по умолчанию:");
        Console.WriteLine("[SpeedometerHUD+WASD] Позиция: X=-2.3, Y=-3.5, Z=6.7");
        Console.WriteLine("[SpeedometerHUD+WASD] Цвет: белый (255,255,255)");
        Console.WriteLine("[SpeedometerHUD+WASD] Метод HUD: point_orient (стабильный, без тряски)");
        Console.WriteLine("[SpeedometerHUD+WASD] Обводка текста: 0.0 0.0 (отключена)");
        Console.WriteLine("[SpeedometerHUD+WASD] Интервал обновления: 0.01с (оптимально)");
        Console.WriteLine("[SpeedometerHUD+WASD] Жирный шрифт: ВКЛЮЧЕН");
        Console.WriteLine("[SpeedometerHUD+WASD] Режим наблюдателя: ВКЛЮЧЕН");
        Console.WriteLine("[SpeedometerHUD+WASD] Работает только с игроками (боты отключены)");
        Console.WriteLine("[SpeedometerHUD+WASD] Время отображения прыжка: 0.4с");
        Console.WriteLine("[SpeedometerHUD+WASD] Окно распрыжки: 0.5с");
        Console.WriteLine("[SpeedometerHUD+WASD] Команды управления:");
        Console.WriteLine("[SpeedometerHUD+WASD]   css_speedometer_settings - текущие настройки");
        Console.WriteLine("[SpeedometerHUD+WASD]   css_speedometer_enable/disable - включить/выключить плагин");
        Console.WriteLine("[SpeedometerHUD+WASD]   css_speedometer_setmethod <0|1> - сменить метод HUD");
        Console.WriteLine("[SpeedometerHUD+WASD]   css_speedometer_setjumptime <0.1-1.0> - время отображения прыжка");
        Console.WriteLine("[SpeedometerHUD+WASD]   css_speedometer_setbhopwindow <0.1-1.0> - окно распрыжки");
        Console.WriteLine("[SpeedometerHUD+WASD]   css_speedometer_test - тестирование плагина");
        Console.WriteLine("[SpeedometerHUD+WASD]   css_speedometer_help - справка по всем командам");
        Console.WriteLine("===============================================");
    }
    
    private HookResult OnPlayerConnectFull(EventPlayerConnectFull @event, GameEventInfo info)
    {
        var player = @event.Userid;
        if (player != null && player.IsValid && !player.IsBot && !player.IsHLTV)
        {
            InitializePlayer(player);
        }
        return HookResult.Continue;
    }
    
    private HookResult OnPlayerSpawn(EventPlayerSpawn @event, GameEventInfo info)
    {
        var player = @event.Userid;
        if (player != null && player.IsValid && !player.IsBot && !player.IsHLTV)
        {
            int slot = player.Slot;
            
            if (IsPlayerActive(player))
            {
                _playerActive[slot] = true;
                _wasOnGround[slot] = true;
                
                Server.NextFrame(() =>
                {
                    if (_userSettings.TryGetValue(slot, out var settings) && settings.IsEnabled)
                    {
                        _hudInitialized[slot] = false;
                        InitializeHUDForPlayer(player);
                    }
                });
            }
        }
        return HookResult.Continue;
    }
    
    private HookResult OnPlayerDeath(EventPlayerDeath @event, GameEventInfo info)
    {
        var player = @event.Userid;
        if (player != null && player.IsValid && !player.IsBot && !player.IsHLTV)
        {
            int slot = player.Slot;
            
            _playerActive[slot] = false;
            _wasOnGround[slot] = false;
            
            if (!Config.ShowSpectator && _hudapi != null && _hudInitialized.ContainsKey(slot) && _hudInitialized[slot])
            {
                _hudapi.Native_GameHUD_Show(player, (byte)Config.HudChannel, " ", 0.01f);
            }
        }
        return HookResult.Continue;
    }
    
    private HookResult OnPlayerTeam(EventPlayerTeam @event, GameEventInfo info)
    {
        var player = @event.Userid;
        if (player != null && player.IsValid && !player.IsBot && !player.IsHLTV)
        {
            int slot = player.Slot;
            int newTeam = @event.Team;
            
            if (newTeam == (int)CsTeam.Spectator || newTeam == (int)CsTeam.None)
            {
                _playerActive[slot] = false;
                _wasOnGround[slot] = false;
                
                if (!Config.ShowSpectator && _hudapi != null && _hudInitialized.ContainsKey(slot) && _hudInitialized[slot])
                {
                    _hudapi.Native_GameHUD_Show(player, (byte)Config.HudChannel, " ", 0.01f);
                }
            }
            else if (newTeam == (int)CsTeam.Terrorist || newTeam == (int)CsTeam.CounterTerrorist)
            {
                _playerActive[slot] = true;
                _wasOnGround[slot] = true;
            }
        }
        return HookResult.Continue;
    }
    
    private HookResult OnPlayerJump(EventPlayerJump @event, GameEventInfo info)
    {
        var player = @event.Userid;
        if (player != null && player.IsValid && !player.IsBot && !player.IsHLTV)
        {
            int slot = player.Slot;
            _lastJumpEvent[slot] = DateTime.Now;
            LogDebug($"Игрок {player.PlayerName} прыгнул (событие)");
        }
        return HookResult.Continue;
    }
    
    private HookResult OnPlayerDisconnect(EventPlayerDisconnect @event, GameEventInfo info)
    {
        var player = @event.Userid;
        if (player != null && player.IsValid && !player.IsBot && !player.IsHLTV)
        {
            int slot = player.Slot;
            _userSettings.TryRemove(slot, out _);
            _hudInitialized.TryRemove(slot, out _);
            _playerActive.TryRemove(slot, out _);
            _previousJumpButton.TryRemove(slot, out _);
            _lastJumpPress.TryRemove(slot, out _);
            _lastJumpEvent.TryRemove(slot, out _);
            _wasOnGround.TryRemove(slot, out _);
            
            if (_hudapi != null)
            {
                _hudapi.Native_GameHUD_Remove(player, (byte)Config.HudChannel);
            }
        }
        return HookResult.Continue;
    }
    
    private HookResult OnRoundStart(EventRoundStart @event, GameEventInfo info)
    {
        // Используем таймер с небольшой задержкой, чтобы игроки успели возродиться
        AddTimer(0.5f, () =>
        {
            foreach (var player in Utilities.GetPlayers())
            {
                if (player != null && player.IsValid && !player.IsBot && !player.IsHLTV)
                {
                    int slot = player.Slot;
                    
                    if (_userSettings.TryGetValue(slot, out var settings) && settings.IsEnabled)
                    {
                        if (IsPlayerActive(player))
                        {
                            _playerActive[slot] = true;
                            _hudInitialized[slot] = false;
                            _wasOnGround[slot] = true;
                            
                            Server.NextFrame(() =>
                            {
                                InitializeHUDForPlayer(player);
                            });
                        }
                    }
                }
            }
            
            // Перезапускаем таймер обновления, если он не активен
            if (Config.Enabled && _updateTimer == null)
            {
                _updateTimer = AddTimer(Config.UpdateInterval, UpdateSpeedometer, 
                    CounterStrikeSharp.API.Modules.Timers.TimerFlags.REPEAT);
                LogDebug("Таймер обновления запущен");
            }
        });
        
        return HookResult.Continue;
    }
    
    private void InitializePlayer(CCSPlayerController player)
    {
        int slot = player.Slot;
        
        _userSettings[slot] = new UserSettings
        {
            IsEnabled = true,
            PlayerSlot = slot
        };
        
        _hudInitialized[slot] = false;
        _playerActive[slot] = true;
        _previousJumpButton[slot] = false;
        _lastJumpPress[slot] = DateTime.MinValue;
        _lastJumpEvent[slot] = DateTime.MinValue;
        _wasOnGround[slot] = true;
        
        LogDebug($"Игрок инициализирован: {player.PlayerName} (слот: {slot})");
        
        Server.NextFrame(() =>
        {
            InitializeHUDForPlayer(player);
        });
    }
    
    private void InitializeHUDForPlayer(CCSPlayerController player)
    {
        if (!Config.Enabled || _hudapi == null || !player.IsValid)
            return;
            
        int slot = player.Slot;
        
        if (!_userSettings.TryGetValue(slot, out var settings) || !settings.IsEnabled)
            return;
        
        if (_hudInitialized.TryGetValue(slot, out bool initialized) && initialized)
            return;
        
        try
        {
            // Удаляем старый HUD если существует
            _hudapi.Native_GameHUD_Remove(player, (byte)Config.HudChannel);
            
            // Определяем имя шрифта с учетом жирности
            string actualFontName = Config.FontName;
            if (Config.UseBoldFont && !actualFontName.EndsWith(" Bold"))
            {
                actualFontName += " Bold";
            }
            
            // Устанавливаем параметры HUD
            _hudapi.Native_GameHUD_SetParams(
                player,
                (byte)Config.HudChannel,
                Config.HudX,
                Config.HudY,
                Config.HudZ,
                _hudColor,
                Config.FontSize,
                actualFontName,
                Config.UnitsPerPixel,
                PointWorldTextJustifyHorizontal_t.POINT_WORLD_TEXT_JUSTIFY_HORIZONTAL_LEFT,
                PointWorldTextJustifyVertical_t.POINT_WORLD_TEXT_JUSTIFY_VERTICAL_TOP,
                PointWorldTextReorientMode_t.POINT_WORLD_TEXT_REORIENT_NONE,
                Config.TextBorderHeight,
                Config.TextBorderWidth
            );
            
            // Показываем пустое сообщение для инициализации
            _hudapi.Native_GameHUD_ShowPermanent(player, (byte)Config.HudChannel, " ");
            
            _hudInitialized[slot] = true;
            LogDebug($"HUD инициализирован для игрока: {player.PlayerName}, метод: {(Config.HudMethod == 1 ? "point_orient" : "teleport")}");
        }
        catch (Exception ex)
        {
            LogError($"Ошибка инициализации HUD для {player.PlayerName}: {ex.Message}");
        }
    }
    
    private void UpdateSpeedometer()
    {
        if (!Config.Enabled || _hudapi == null)
            return;
        
        try
        {
            foreach (var player in Utilities.GetPlayers())
            {
                if (player == null || !player.IsValid || player.IsBot || player.IsHLTV)
                    continue;
                    
                int slot = player.Slot;
                
                if (!_userSettings.TryGetValue(slot, out var settings) || !settings.IsEnabled)
                    continue;
                
                if (!_hudInitialized.TryGetValue(slot, out bool initialized) || !initialized)
                {
                    InitializeHUDForPlayer(player);
                    continue;
                }
                
                bool isAlive = player.PawnIsAlive;
                bool isSpectator = player.TeamNum == (byte)CsTeam.Spectator || player.TeamNum == (byte)CsTeam.None;
                
                CCSPlayerController targetPlayer = player;
                int targetSlot = slot;
                
                // Если игрок мертв или в режиме наблюдателя, и включен режим наблюдения
                if ((!isAlive || isSpectator) && Config.ShowSpectator)
                {
                    // Получаем цель, за которой наблюдает игрок
                    CCSPlayerController? observerTarget = GetObserverTarget(player);
                    
                    if (observerTarget != null && observerTarget.IsValid && !observerTarget.IsBot && !observerTarget.IsHLTV)
                    {
                        targetPlayer = observerTarget;
                        targetSlot = targetPlayer.Slot;
                        LogDebug($"Игрок {player.PlayerName} наблюдает за {targetPlayer.PlayerName}");
                    }
                    else
                    {
                        // Не показываем HUD, если цель не найдена или это бот
                        _hudapi.Native_GameHUD_Show(player, (byte)Config.HudChannel, " ", 0.01f);
                        continue;
                    }
                }
                else if (!isAlive || isSpectator)
                {
                    // Игрок мертв или в режиме наблюдателя, но режим наблюдения выключен
                    _hudapi.Native_GameHUD_Show(player, (byte)Config.HudChannel, " ", 0.01f);
                    continue;
                }
                
                // Получаем данные цели (собственные или наблюдаемого игрока)
                var pawn = targetPlayer.PlayerPawn?.Value;
                if (pawn == null || !pawn.IsValid)
                    continue;
                
                float speed = 0.0f;
                if (pawn.AbsVelocity != null)
                {
                    speed = (float)Math.Round(pawn.AbsVelocity.Length2D());
                }
                
                // Обновляем состояние прыжка
                UpdateJumpState(targetPlayer, targetSlot);
                
                string message;
                
                if (Config.ShowKeys)
                {
                    string speedStr = speed.ToString();
                    string keysDisplay = GetMovementKeysDisplay(targetPlayer, targetSlot);
                    
                    // Центрируем скорость над клавишами
                    int totalWidth = 18;
                    int speedLength = speedStr.Length;
                    int leftPadding = (totalWidth - speedLength) / 2;
                    int rightPadding = totalWidth - speedLength - leftPadding;
                    string centeredSpeed = new string(' ', leftPadding) + speedStr + new string(' ', rightPadding);
                    
                    // Добавляем метку, если наблюдаем за другим игроком
                    string observerLabel = "";
                    if (targetPlayer != player)
                    {
                        string playerName = targetPlayer.PlayerName.Length > 10 
                            ? targetPlayer.PlayerName.Substring(0, 10) + "..." 
                            : targetPlayer.PlayerName;
                        observerLabel = $"[{playerName}]\n";
                    }
                    
                    message = $"{observerLabel}{centeredSpeed}\n{keysDisplay}";
                }
                else
                {
                    string observerLabel = targetPlayer != player ? $"[Наблюдение: {targetPlayer.PlayerName}]\n" : "";
                    message = $"{observerLabel}{speed}";
                }
                
                _hudapi.Native_GameHUD_ShowPermanent(player, (byte)Config.HudChannel, message);
            }
        }
        catch (Exception ex)
        {
            LogError($"Ошибка в UpdateSpeedometer: {ex.Message}");
        }
    }
    
    private void UpdateJumpState(CCSPlayerController player, int slot)
    {
        try
        {
            var pawn = player.PlayerPawn?.Value;
            if (pawn == null || !pawn.IsValid)
                return;
            
            // Проверяем состояние кнопки прыжка
            bool currentJumpButton = (player.Buttons & PlayerButtons.Jump) != 0;
            bool previousJumpButton = _previousJumpButton.GetOrAdd(slot, false);
            
            // Фиксируем нажатие кнопки прыжка (когда кнопка становится нажатой)
            if (currentJumpButton && !previousJumpButton)
            {
                _lastJumpPress[slot] = DateTime.Now;
                LogDebug($"Игрок {player.PlayerName} нажал кнопку прыжка (Space или MWHEELDOWN)");
            }
            
            // Сохраняем текущее состояние для следующего обновления
            _previousJumpButton[slot] = currentJumpButton;
            
            // Проверяем состояние на земле
            bool isOnGround = (pawn.Flags & (1 << 0)) != 0; // FL_ONGROUND
            
            // Обновляем состояние "был на земле"
            bool wasOnGround = _wasOnGround.GetOrAdd(slot, true);
            
            // Если игрок был на земле и теперь в воздухе - это тоже прыжок
            if (wasOnGround && !isOnGround)
            {
                // Если это не было зафиксировано через событие, отмечаем как прыжок
                if (!_lastJumpEvent.ContainsKey(slot) || 
                    (DateTime.Now - _lastJumpEvent[slot]).TotalSeconds > 0.1)
                {
                    _lastJumpPress[slot] = DateTime.Now;
                    LogDebug($"Игрок {player.PlayerName} оторвался от земли (определено через FL_ONGROUND)");
                }
            }
            
            // Обновляем состояние на земле
            _wasOnGround[slot] = isOnGround;
        }
        catch (Exception ex)
        {
            LogError($"Ошибка в UpdateJumpState для игрока {player.PlayerName}: {ex.Message}");
        }
    }
    
    private CCSPlayerController? GetObserverTarget(CCSPlayerController player)
    {
        try
        {
            var pawn = player.PlayerPawn?.Value;
            if (pawn == null || !pawn.IsValid)
                return null;
            
            if (pawn.ObserverServices == null)
                return null;
            
            var targetHandle = pawn.ObserverServices.ObserverTarget;
            if (targetHandle == null)
                return null;
            
            var targetEntity = targetHandle.Value;
            if (targetEntity == null || !targetEntity.IsValid)
                return null;
            
            int targetIndex = (int)targetEntity.Index;
            
            foreach (var potentialTarget in Utilities.GetPlayers())
            {
                if (potentialTarget == null || !potentialTarget.IsValid || potentialTarget.IsBot || potentialTarget.IsHLTV)
                    continue;
                    
                var targetPawn = potentialTarget.PlayerPawn?.Value;
                if (targetPawn != null && targetPawn.IsValid && (int)targetPawn.Index == targetIndex)
                {
                    return potentialTarget;
                }
            }
            
            return null;
        }
        catch (Exception ex)
        {
            LogDebug($"Ошибка при получении цели наблюдателя: {ex.Message}");
            return null;
        }
    }
    
    private string GetMovementKeysDisplay(CCSPlayerController player, int slot)
    {
        var buttons = (ulong)player.Buttons;
        
        // Верхняя строка: TA пусто W E R
        string ta = (buttons & SCORE_BUTTON) != 0 ? "TA" : "__";
        string empty = "__";
        string w = (player.Buttons & PlayerButtons.Forward) != 0 ? "W " : "__";
        string e = (player.Buttons & PlayerButtons.Use) != 0 ? "E " : "__";
        string r = (player.Buttons & PlayerButtons.Reload) != 0 ? "R " : "__";
        string topRow = $"{ta}  {empty}  {w}  {e}  {r}";
        
        // Средняя строка: SH A S D F
        string shift = (player.Buttons & PlayerButtons.Speed) != 0 ? "SH" : "__";
        string a = (player.Buttons & PlayerButtons.Moveleft) != 0 ? "A " : "__";
        string s = (player.Buttons & PlayerButtons.Back) != 0 ? "S " : "__";
        string d = (player.Buttons & PlayerButtons.Moveright) != 0 ? "D " : "__";
        string f = (buttons & INSPECT_BUTTON) != 0 ? "F " : "__";
        string middleRow = $"{shift}  {a}  {s}  {d}  {f}";
        
        // Нижняя строка: DU пусто JU пусто пусто
        string du = (player.Buttons & PlayerButtons.Duck) != 0 ? "DU" : "__";
        
        // Определяем, прыгает ли игрок:
        bool isJumping = false;
        
        // Вариант 1: Кнопка прыжка нажата сейчас (Space)
        bool jumpButtonPressed = (player.Buttons & PlayerButtons.Jump) != 0;
        
        // Вариант 2: Событие прыжка было недавно
        bool jumpEventRecent = false;
        if (_lastJumpEvent.TryGetValue(slot, out var lastEvent))
        {
            jumpEventRecent = (DateTime.Now - lastEvent).TotalSeconds < Config.JumpDisplayTime;
        }
        
        // Вариант 3: Нажатие кнопки прыжка было недавно (Space или MWHEELDOWN)
        bool jumpPressRecent = false;
        if (_lastJumpPress.TryGetValue(slot, out var lastPress))
        {
            jumpPressRecent = (DateTime.Now - lastPress).TotalSeconds < Config.JumpDisplayTime;
        }
        
        // Вариант 4: Проверка для распрыжки
        // Если мы в окне распрыжки (недавно был прыжок и нажимаем кнопку прыжка снова)
        bool isBunnyhopping = false;
        if (jumpPressRecent && jumpButtonPressed)
        {
            // Если был недавний прыжок и кнопка снова нажата - это распрыжка
            isBunnyhopping = true;
            LogDebug($"Игрок {player.PlayerName} в режиме распрыжки");
        }
        
        // Показываем JU, если:
        // 1. Кнопка прыжка нажата сейчас (Space держится)
        // 2. ИЛИ было событие прыжка недавно
        // 3. ИЛИ было нажатие кнопки прыжка недавно (Space или MWHEELDOWN)
        // 4. ИЛИ мы в режиме распрыжки
        if (jumpButtonPressed || jumpEventRecent || jumpPressRecent || isBunnyhopping)
        {
            isJumping = true;
        }
        
        string ju = isJumping ? "JU" : "__";
        string bottomRow = $"{du}  {empty}  {ju}  {empty}  {empty}";
        
        return $"{topRow}\n{middleRow}\n{bottomRow}";
    }
    
    private bool IsPlayerActive(CCSPlayerController player)
    {
        if (player == null || !player.IsValid)
            return false;
            
        bool isAlive = player.PawnIsAlive;
        bool isSpectator = player.TeamNum == (byte)CsTeam.Spectator || player.TeamNum == (byte)CsTeam.None;
        
        return isAlive && !isSpectator;
    }
    
    private void OnHelpCommand(CCSPlayerController? player, CommandInfo command)
    {
        string helpMessage = """
            ================================================
            СПРАВКА ПО ПЛАГИНУ SPEEDOMETERHUD+WASD v1.9
            ================================================
            ОПИСАНИЕ:
              Отображает скорость игрока и нажатые клавиши движения в 3D HUD.
              Использует стабильный метод point_orient для устранения тряски.
              Скорость отображается по центру над клавишами.
              Работает в игре, после смерти и в режиме наблюдателя.
              Показывает скорость и клавиши наблюдаемого игрока (только реальных игроков, не ботов).
              Улучшенный алгоритм определения прыжков работает для Space и MWHEELDOWN.

            ОСНОВНЫЕ КОМАНДЫ (значения по умолчанию в скобках):
              css_speedometer_help - Показать эту справку
              css_speedometer_settings - Показать текущие настройки
              css_speedometer_test - Тестирование работы плагина
              css_speedometer_enable - Включить плагин (по умолчанию: включено)
              css_speedometer_disable - Выключить плагин
              css_speedometer_reload - Перезагрузить конфигурацию из файла
              css_speedometer_toggle - ВКЛ/ВЫКЛ отображение для себя

            КОМАНДЫ ДЛЯ ВСЕХ ПЕРЕМЕННЫХ КОНФИГА:
              css_speedometer_setenabled <0/1> - Включить/выключить плагин (по умолчанию: 1)
              css_speedometer_setchannel <0-255> - Канал HUD (по умолчанию: 1)
              css_speedometer_setposition <X Y Z> - Позиция HUD (по умолчанию: -2.3 -3.5 6.7)
              css_speedometer_setcolor <R,G,B> - Цвет HUD (по умолчанию: 255,255,255)
              css_speedometer_setfontsize <10-100> - Размер шрифта (по умолчанию: 40)
              css_speedometer_setfontname <имя> - Имя шрифта (по умолчанию: Consolas)
              css_speedometer_setunits <0.001-1.0> - Единиц на пиксель (по умолчанию: 0.0057)
              css_speedometer_setinterval <0.01-1.0> - Интервал обновления (по умолчанию: 0.01)
              css_speedometer_setkeys <0/1> - Показывать клавиши движения (по умолчанию: 1)
              css_speedometer_setspectator <0/1> - Показывать в режиме наблюдателя (по умолчанию: 1)
              css_speedometer_setloglevel <0-2> - Уровень логирования (по умолчанию: 0)
              css_speedometer_setborder <ширина> <высота> - Обводка текста (по умолчанию: 0.0 0.0)
              css_speedometer_setbold <0/1> - Использовать жирный шрифт (по умолчанию: 1)
              css_speedometer_setmethod <0/1> - Метод HUD (0-телепорт, 1-point_orient) (по умолчанию: 1)
              css_speedometer_setjumptime <0.1-1.0> - Время отображения прыжка (по умолчанию: 0.4)
              css_speedometer_setbhopwindow <0.1-1.0> - Окно распрыжки (по умолчанию: 0.5)

            ПСЕВДОНИМЫ КОМАНД:
              css_speedometer_togglekeys - ВКЛ/ВЫКЛ отображение клавиш (по умолчанию: включено)
              css_speedometer_togglespectator - ВКЛ/ВЫКЛ отображение в режиме наблюдателя (по умолчанию: включено)
              css_speedometer_togglebold - ВКЛ/ВЫКЛ жирный шрифт (по умолчанию: включено)

            ВАЖНО: Для десятичных чисел используйте ТОЧКУ (.) а не запятую!
            Примеры: css_speedometer_setbhopwindow 0.5
                     css_speedometer_setposition -2.3 -3.5 6.7
                     css_speedometer_setinterval 0.01

            СОВЕТЫ ПО НАСТРОЙКЕ:
              Для устранения тряски используйте: css_speedometer_setmethod 1 (point_orient)
              Для отключения обводки: css_speedometer_setborder 0.0 0.0
              Рекомендуемые шрифты: "Consolas Bold", "Arial Black", "Impact"
              Если JU плохо отображается: увеличьте css_speedometer_setjumptime до 0.6
              Интервал 0.01 обеспечивает плавное отображение (рекомендуется)
              Обводка отключена по умолчанию для чистого вида
            ================================================
            """;
        
        if (player != null)
        {
            player.PrintToConsole(helpMessage);
        }
        else
        {
            command.ReplyToCommand(helpMessage);
        }
    }
    
    private void OnSettingsCommand(CCSPlayerController? player, CommandInfo command)
    {
        int enabledPlayers = _userSettings.Count(kvp => kvp.Value.IsEnabled);
        int totalPlayers = _userSettings.Count;
        
        string settingsMessage = $"""
            ================================================
            SPEEDOMETERHUD+WASD v{ModuleVersion} - ТЕКУЩИЕ НАСТРОЙКИ
            ================================================
            Плагин включен: {Config.Enabled}
            Канал HUD: {Config.HudChannel} (по умолчанию: 1)
            Позиция HUD: X={Config.HudX}, Y={Config.HudY}, Z={Config.HudZ} (по умолчанию: -2.3 -3.5 6.7)
            Шрифт: {Config.FontName} {Config.FontSize}px (по умолчанию: Consolas 40)
            Единиц на пиксель: {Config.UnitsPerPixel:F6} (по умолчанию: 0.0057)
            Цвет: {Config.Color} (по умолчанию: 255,255,255)
            Интервал обновления: {Config.UpdateInterval}с (по умолчанию: 0.01)
            Показывать клавиши: {Config.ShowKeys} (по умолчанию: true)
            Показывать в режиме наблюдателя: {Config.ShowSpectator} (по умолчанию: true)
            Обводка текста: ширина={Config.TextBorderWidth}, высота={Config.TextBorderHeight} (по умолчанию: 0.0 0.0)
            Жирный шрифт: {(Config.UseBoldFont ? "Включен" : "Выключен")} (по умолчанию: включен)
            Метод HUD: {Config.HudMethod} ({(Config.HudMethod == 1 ? "point_orient (стабильный)" : "teleport (быстрый)")}) (по умолчанию: 1)
            Время отображения прыжка: {Config.JumpDisplayTime}с (по умолчанию: 0.4)
            Окно распрыжки: {Config.BunnyhopWindow}с (по умолчанию: 0.5)
            Уровень логирования: {Config.LogLevel} (по умолчанию: 0)
            GameHUD API: {(_hudapi != null ? "Доступно" : "Не доступно")}
            Игроков со спидометром: {enabledPlayers}/{totalPlayers}
            Таймер обновления: {(_updateTimer != null ? "Активен" : "Не активен")}
            Работает только с игроками: Да (боты отключены)
            Алгоритм прыжков: Комбинированный (событие + кнопка + состояние на земле)
            ================================================
            """;
        
        if (player != null)
        {
            player.PrintToConsole(settingsMessage);
        }
        else
        {
            command.ReplyToCommand(settingsMessage);
        }
    }
    
    private void OnReloadCommand(CCSPlayerController? player, CommandInfo command)
    {
        string message = "Конфигурация успешно перезагружена!";
        command.ReplyToCommand($"[SpeedometerHUD+WASD] {message}");
        
        if (_updateTimer != null)
        {
            _updateTimer.Kill();
            _updateTimer = null;
        }
        
        // Обновляем метод HUD
        SetGameHUDMethod(Config.HudMethod == 1);
        
        if (Config.Enabled)
        {
            _updateTimer = AddTimer(Config.UpdateInterval, UpdateSpeedometer, 
                CounterStrikeSharp.API.Modules.Timers.TimerFlags.REPEAT);
        }
        
        // Переинициализируем HUD для всех игроков
        foreach (var kvp in _userSettings)
        {
            var settings = kvp.Value;
            if (settings.IsEnabled)
            {
                var playerObj = Utilities.GetPlayerFromSlot(settings.PlayerSlot);
                if (playerObj != null && playerObj.IsValid && !playerObj.IsBot)
                {
                    _hudInitialized[settings.PlayerSlot] = false;
                    Server.NextFrame(() => InitializeHUDForPlayer(playerObj));
                }
            }
        }
    }
    
    private void OnToggleCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (player == null)
        {
            command.ReplyToCommand("[SpeedometerHUD+WASD] Эта команда может использоваться только игроками");
            return;
        }
        
        int slot = player.Slot;
        if (!_userSettings.TryGetValue(slot, out var settings))
        {
            command.ReplyToCommand("[SpeedometerHUD+WASD] Игрок не инициализирован");
            return;
        }
        
        settings.IsEnabled = !settings.IsEnabled;
        _userSettings[slot] = settings;
        
        string message = $"Отображение спидометра {(settings.IsEnabled ? "включено" : "выключено")}";
        command.ReplyToCommand($"[SpeedometerHUD+WASD] {message}");
        
        if (settings.IsEnabled)
        {
            _hudInitialized[slot] = false;
            _playerActive[slot] = true;
            Server.NextFrame(() => InitializeHUDForPlayer(player));
        }
        else
        {
            if (_hudapi != null)
            {
                _hudapi.Native_GameHUD_Remove(player, (byte)Config.HudChannel);
            }
            _hudInitialized[slot] = false;
            _playerActive[slot] = false;
        }
        
        LogInfo($"Игрок {player.PlayerName} переключил спидометр: {settings.IsEnabled}");
    }
    
    // Команды для всех переменных конфига
    
    private void OnSetEnabledCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (command.ArgCount < 2)
        {
            command.ReplyToCommand($"Плагин включен: {Config.Enabled} (по умолчанию: true)");
            command.ReplyToCommand($"Использование: css_speedometer_setenabled <0/1>");
            return;
        }
        
        if (int.TryParse(command.GetArg(1), out int enabled))
        {
            bool oldValue = Config.Enabled;
            Config.Enabled = enabled == 1;
            
            string message = $"Плагин {(Config.Enabled ? "включен" : "выключен")} (по умолчанию: включен)";
            command.ReplyToCommand($"[SpeedometerHUD+WASD] {message}");
            
            SaveConfigInternal();
            
            if (Config.Enabled && _updateTimer == null)
            {
                _updateTimer = AddTimer(Config.UpdateInterval, UpdateSpeedometer, 
                    CounterStrikeSharp.API.Modules.Timers.TimerFlags.REPEAT);
            }
            else if (!Config.Enabled && _updateTimer != null)
            {
                _updateTimer.Kill();
                _updateTimer = null;
                
                // Удаляем HUD у всех игроков
                if (_hudapi != null)
                {
                    foreach (var playerObj in Utilities.GetPlayers())
                    {
                        if (playerObj != null && playerObj.IsValid)
                        {
                            _hudapi.Native_GameHUD_Remove(playerObj, (byte)Config.HudChannel);
                        }
                    }
                }
            }
            
            LogInfo(message);
        }
        else
        {
            command.ReplyToCommand("[SpeedometerHUD+WASD] Неверное значение. Использование: css_speedometer_setenabled <0/1>");
        }
    }
    
    private void OnSetChannelCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (command.ArgCount < 2)
        {
            command.ReplyToCommand($"Текущий канал HUD: {Config.HudChannel} (по умолчанию: 1)");
            command.ReplyToCommand($"Использование: css_speedometer_setchannel <0-255>");
            return;
        }
        
        if (int.TryParse(command.GetArg(1), out int newChannel))
        {
            newChannel = Math.Clamp(newChannel, 0, 255);
            int oldChannel = Config.HudChannel;
            Config.HudChannel = newChannel;
            
            string message = $"Канал HUD изменен с {oldChannel} на {newChannel} (по умолчанию: 1)";
            command.ReplyToCommand($"[SpeedometerHUD+WASD] {message}");
            
            SaveConfigInternal();
            
            foreach (var kvp in _userSettings)
            {
                var settings = kvp.Value;
                if (settings.IsEnabled)
                {
                    var playerObj = Utilities.GetPlayerFromSlot(settings.PlayerSlot);
                    if (playerObj != null && playerObj.IsValid && !playerObj.IsBot && _hudapi != null)
                    {
                        _hudapi.Native_GameHUD_Remove(playerObj, (byte)oldChannel);
                        _hudInitialized[settings.PlayerSlot] = false;
                        Server.NextFrame(() => InitializeHUDForPlayer(playerObj));
                    }
                }
            }
            
            LogInfo(message);
        }
        else
        {
            command.ReplyToCommand("[SpeedometerHUD+WASD] Неверный номер канала. Использование: css_speedometer_setchannel <0-255>");
        }
    }
    
    private void OnSetPositionCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (command.ArgCount < 2)
        {
            command.ReplyToCommand($"Текущая позиция HUD: X={Config.HudX}, Y={Config.HudY}, Z={Config.HudZ} (по умолчанию: -2.3 -3.5 6.7)");
            command.ReplyToCommand($"Использование: css_speedometer_setposition <X> <Y> <Z>");
            command.ReplyToCommand($"Пример: css_speedometer_setposition -2.3 -3.5 6.7");
            return;
        }
        
        // Получаем строку аргументов
        string argsString = command.ArgString.Trim();
        string[] args = argsString.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        
        if (args.Length < 3)
        {
            command.ReplyToCommand("[SpeedometerHUD+WASD] Неверное количество аргументов. Использование: css_speedometer_setposition <X> <Y> <Z>");
            command.ReplyToCommand($"Пример: css_speedometer_setposition -2.3 -3.5 6.7");
            return;
        }
        
        // Парсим с использованием InvariantCulture, чтобы правильно обрабатывать точки
        if (float.TryParse(args[0], NumberStyles.Float, CultureInfo.InvariantCulture, out float x) &&
            float.TryParse(args[1], NumberStyles.Float, CultureInfo.InvariantCulture, out float y) &&
            float.TryParse(args[2], NumberStyles.Float, CultureInfo.InvariantCulture, out float z))
        {
            x = Math.Clamp(x, -100.0f, 100.0f);
            y = Math.Clamp(y, -100.0f, 100.0f);
            z = Math.Clamp(z, 0.0f, 20.0f);
            
            float oldX = Config.HudX;
            float oldY = Config.HudY;
            float oldZ = Config.HudZ;
            
            Config.HudX = x;
            Config.HudY = y;
            Config.HudZ = z;
            
            string message = $"Позиция HUD изменена с ({oldX},{oldY},{oldZ}) на ({x},{y},{z}) (по умолчанию: -2.3,-3.5,6.7)";
            command.ReplyToCommand($"[SpeedometerHUD+WASD] {message}");
            
            SaveConfigInternal();
            
            foreach (var kvp in _userSettings)
            {
                var settings = kvp.Value;
                if (settings.IsEnabled)
                {
                    var playerObj = Utilities.GetPlayerFromSlot(settings.PlayerSlot);
                    if (playerObj != null && playerObj.IsValid && !playerObj.IsBot)
                    {
                        _hudInitialized[settings.PlayerSlot] = false;
                        Server.NextFrame(() => InitializeHUDForPlayer(playerObj));
                    }
                }
            }
            
            LogInfo(message);
        }
        else
        {
            command.ReplyToCommand("[SpeedometerHUD+WASD] Неверные значения позиции. Использование: css_speedometer_setposition <X> <Y> <Z>");
            command.ReplyToCommand("ВАЖНО: Используйте точку (.) для десятичных чисел, а не запятую!");
        }
    }
    
    private void OnSetColorCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (command.ArgCount < 2)
        {
            command.ReplyToCommand($"Текущий цвет HUD: {Config.Color} (по умолчанию: 255,255,255)");
            command.ReplyToCommand($"Использование: css_speedometer_setcolor <R,G,B>");
            return;
        }
        
        string newColor = command.GetArg(1);
        try
        {
            var colorParts = newColor.Split(',');
            if (colorParts.Length == 3)
            {
                int r = Math.Clamp(int.Parse(colorParts[0]), 0, 255);
                int g = Math.Clamp(int.Parse(colorParts[1]), 0, 255);
                int b = Math.Clamp(int.Parse(colorParts[2]), 0, 255);
                
                string oldColor = Config.Color;
                Config.Color = $"{r},{g},{b}";
                _hudColor = Color.FromArgb(r, g, b);
                
                string message = $"Цвет HUD изменен с {oldColor} на {Config.Color} (по умолчанию: 255,255,255)";
                command.ReplyToCommand($"[SpeedometerHUD+WASD] {message}");
                
                SaveConfigInternal();
                
                foreach (var kvp in _userSettings)
                {
                    var settings = kvp.Value;
                    if (settings.IsEnabled)
                    {
                        var playerObj = Utilities.GetPlayerFromSlot(settings.PlayerSlot);
                        if (playerObj != null && playerObj.IsValid && !playerObj.IsBot)
                        {
                            _hudInitialized[settings.PlayerSlot] = false;
                            Server.NextFrame(() => InitializeHUDForPlayer(playerObj));
                        }
                    }
                }
                
                LogInfo(message);
            }
            else
            {
                command.ReplyToCommand("[SpeedometerHUD+WASD] Неверный формат цвета. Использование: css_speedometer_setcolor <R,G,B>");
            }
        }
        catch
        {
            command.ReplyToCommand("[SpeedometerHUD+WASD] Неверные значения цвета. Использование: css_speedometer_setcolor <R,G,B>");
        }
    }
    
    private void OnSetFontSizeCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (command.ArgCount < 2)
        {
            command.ReplyToCommand($"Текущий размер шрифта: {Config.FontSize} (по умолчанию: 40)");
            command.ReplyToCommand($"Использование: css_speedometer_setfontsize <10-100>");
            return;
        }
        
        if (int.TryParse(command.GetArg(1), out int newSize))
        {
            newSize = Math.Clamp(newSize, 10, 100);
            int oldSize = Config.FontSize;
            Config.FontSize = newSize;
            
            string message = $"Размер шрифта изменен с {oldSize} на {newSize} (по умолчанию: 40)";
            command.ReplyToCommand($"[SpeedometerHUD+WASD] {message}");
            
            SaveConfigInternal();
            
            foreach (var kvp in _userSettings)
            {
                var settings = kvp.Value;
                if (settings.IsEnabled)
                {
                    var playerObj = Utilities.GetPlayerFromSlot(settings.PlayerSlot);
                    if (playerObj != null && playerObj.IsValid && !playerObj.IsBot)
                    {
                        _hudInitialized[settings.PlayerSlot] = false;
                        Server.NextFrame(() => InitializeHUDForPlayer(playerObj));
                    }
                }
            }
            
            LogInfo(message);
        }
        else
        {
            command.ReplyToCommand("[SpeedometerHUD+WASD] Неверный размер шрифта. Использование: css_speedometer_setfontsize <10-100>");
        }
    }
    
    private void OnSetFontNameCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (command.ArgCount < 2)
        {
            command.ReplyToCommand($"Текущее имя шрифта: {Config.FontName} (по умолчанию: Consolas)");
            command.ReplyToCommand($"Использование: css_speedometer_setfontname <имя шрифта>");
            return;
        }
        
        string newFontName = command.GetArg(1);
        if (!string.IsNullOrWhiteSpace(newFontName))
        {
            string oldFontName = Config.FontName;
            Config.FontName = newFontName;
            
            string message = $"Имя шрифта изменено с '{oldFontName}' на '{newFontName}' (по умолчанию: Consolas)";
            command.ReplyToCommand($"[SpeedometerHUD+WASD] {message}");
            
            SaveConfigInternal();
            
            foreach (var kvp in _userSettings)
            {
                var settings = kvp.Value;
                if (settings.IsEnabled)
                {
                    var playerObj = Utilities.GetPlayerFromSlot(settings.PlayerSlot);
                    if (playerObj != null && playerObj.IsValid && !playerObj.IsBot)
                    {
                        _hudInitialized[settings.PlayerSlot] = false;
                        Server.NextFrame(() => InitializeHUDForPlayer(playerObj));
                    }
                }
            }
            
            LogInfo(message);
        }
        else
        {
            command.ReplyToCommand("[SpeedometerHUD+WASD] Неверное имя шрифта. Использование: css_speedometer_setfontname <имя шрифта>");
        }
    }
    
    private void OnSetUnitsCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (command.ArgCount < 2)
        {
            command.ReplyToCommand($"Текущие единицы на пиксель: {Config.UnitsPerPixel:F6} (по умолчанию: 0.0057)");
            command.ReplyToCommand($"Использование: css_speedometer_setunits <0.001-1.0>");
            command.ReplyToCommand($"Пример: css_speedometer_setunits 0.0057");
            return;
        }
        
        // Получаем строку аргументов и парсим с использованием InvariantCulture
        string arg = command.GetArg(1).Replace(',', '.'); // Заменяем запятую на точку для совместимости
        
        if (float.TryParse(arg, NumberStyles.Float, CultureInfo.InvariantCulture, out float newUnits))
        {
            newUnits = Math.Clamp(newUnits, 0.001f, 1.0f);
            float oldUnits = Config.UnitsPerPixel;
            Config.UnitsPerPixel = newUnits;
            
            string message = $"Единиц на пиксель изменено с {oldUnits:F6} на {newUnits:F6} (по умолчанию: 0.0057)";
            command.ReplyToCommand($"[SpeedometerHUD+WASD] {message}");
            
            SaveConfigInternal();
            
            foreach (var kvp in _userSettings)
            {
                var settings = kvp.Value;
                if (settings.IsEnabled)
                {
                    var playerObj = Utilities.GetPlayerFromSlot(settings.PlayerSlot);
                    if (playerObj != null && playerObj.IsValid && !playerObj.IsBot)
                    {
                        _hudInitialized[settings.PlayerSlot] = false;
                        Server.NextFrame(() => InitializeHUDForPlayer(playerObj));
                    }
                }
            }
            
            LogInfo(message);
        }
        else
        {
            command.ReplyToCommand("[SpeedometerHUD+WASD] Неверное значение. Использование: css_speedometer_setunits <0.001-1.0>");
            command.ReplyToCommand("ВАЖНО: Используйте точку (.) для десятичных чисел, например: 0.0057");
        }
    }
    
    private void OnSetIntervalCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (command.ArgCount < 2)
        {
            command.ReplyToCommand($"Текущий интервал обновления: {Config.UpdateInterval}с (по умолчанию: 0.01)");
            command.ReplyToCommand($"Использование: css_speedometer_setinterval <0.01-1.0>");
            command.ReplyToCommand($"Пример: css_speedometer_setinterval 0.01");
            return;
        }
        
        // Получаем строку аргументов и парсим с использованием InvariantCulture
        string arg = command.GetArg(1).Replace(',', '.'); // Заменяем запятую на точку для совместимости
        
        if (float.TryParse(arg, NumberStyles.Float, CultureInfo.InvariantCulture, out float newInterval))
        {
            newInterval = Math.Clamp(newInterval, 0.01f, 1.0f);
            float oldInterval = Config.UpdateInterval;
            Config.UpdateInterval = newInterval;
            
            SaveConfigInternal();
            
            if (_updateTimer != null)
            {
                _updateTimer.Kill();
                _updateTimer = null;
            }
            
            if (Config.Enabled)
            {
                _updateTimer = AddTimer(Config.UpdateInterval, UpdateSpeedometer, 
                    CounterStrikeSharp.API.Modules.Timers.TimerFlags.REPEAT);
            }
            
            string message = $"Интервал обновления изменен с {oldInterval}с на {newInterval}с (по умолчанию: 0.01)";
            command.ReplyToCommand($"[SpeedometerHUD+WASD] {message}");
            
            LogInfo(message);
        }
        else
        {
            command.ReplyToCommand("[SpeedometerHUD+WASD] Неверный интервал. Использование: css_speedometer_setinterval <0.01-1.0>");
            command.ReplyToCommand("ВАЖНО: Используйте точку (.) для десятичных чисел, например: 0.01");
        }
    }
    
    private void OnSetKeysCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (command.ArgCount < 2)
        {
            command.ReplyToCommand($"Показывать клавиши движения: {Config.ShowKeys} (по умолчанию: true)");
            command.ReplyToCommand($"Использование: css_speedometer_setkeys <0/1>");
            return;
        }
        
        if (int.TryParse(command.GetArg(1), out int showKeys))
        {
            bool oldValue = Config.ShowKeys;
            Config.ShowKeys = showKeys == 1;
            
            string message = $"Отображение клавиш движения {(Config.ShowKeys ? "включено" : "выключено")} (по умолчанию: включено)";
            command.ReplyToCommand($"[SpeedometerHUD+WASD] {message}");
            
            SaveConfigInternal();
            LogInfo(message);
        }
        else
        {
            command.ReplyToCommand("[SpeedometerHUD+WASD] Неверное значение. Использование: css_speedometer_setkeys <0/1>");
        }
    }
    
    private void OnSetSpectatorCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (command.ArgCount < 2)
        {
            command.ReplyToCommand($"Показывать в режиме наблюдателя: {Config.ShowSpectator} (по умолчанию: true)");
            command.ReplyToCommand($"Использование: css_speedometer_setspectator <0/1>");
            return;
        }
        
        if (int.TryParse(command.GetArg(1), out int showSpectator))
        {
            bool oldValue = Config.ShowSpectator;
            Config.ShowSpectator = showSpectator == 1;
            
            string message = $"Отображение в режиме наблюдателя {(Config.ShowSpectator ? "включено" : "выключено")} (по умолчанию: включено)";
            command.ReplyToCommand($"[SpeedometerHUD+WASD] {message}");
            
            SaveConfigInternal();
            LogInfo(message);
            
            // Обновляем HUD для всех игроков при изменении этой настройки
            foreach (var kvp in _userSettings)
            {
                var settings = kvp.Value;
                if (settings.IsEnabled)
                {
                    var playerObj = Utilities.GetPlayerFromSlot(settings.PlayerSlot);
                    if (playerObj != null && playerObj.IsValid && !playerObj.IsBot)
                    {
                        _hudInitialized[settings.PlayerSlot] = false;
                        Server.NextFrame(() => InitializeHUDForPlayer(playerObj));
                    }
                }
            }
        }
        else
        {
            command.ReplyToCommand("[SpeedometerHUD+WASD] Неверное значение. Использование: css_speedometer_setspectator <0/1>");
        }
    }
    
    private void OnSetBorderCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (command.ArgCount < 3)
        {
            command.ReplyToCommand($"Текущая обводка: ширина={Config.TextBorderWidth}, высота={Config.TextBorderHeight} (по умолчанию: 0.0 0.0)");
            command.ReplyToCommand($"Использование: css_speedometer_setborder <ширина 0-10> <высота 0-10>");
            command.ReplyToCommand($"Пример: css_speedometer_setborder 0.0 0.0");
            return;
        }
        
        // Получаем строки аргументов и парсим с использованием InvariantCulture
        string arg1 = command.GetArg(1).Replace(',', '.'); // Заменяем запятую на точку
        string arg2 = command.GetArg(2).Replace(',', '.'); // Заменяем запятую на точку
        
        if (float.TryParse(arg1, NumberStyles.Float, CultureInfo.InvariantCulture, out float width) &&
            float.TryParse(arg2, NumberStyles.Float, CultureInfo.InvariantCulture, out float height))
        {
            width = Math.Clamp(width, 0.0f, 10.0f);
            height = Math.Clamp(height, 0.0f, 10.0f);
            
            float oldWidth = Config.TextBorderWidth;
            float oldHeight = Config.TextBorderHeight;
            
            Config.TextBorderWidth = width;
            Config.TextBorderHeight = height;
            
            string message = $"Обводка текста изменена с ({oldWidth},{oldHeight}) на ({width},{height}) (по умолчанию: 0.0,0.0)";
            command.ReplyToCommand($"[SpeedometerHUD+WASD] {message}");
            
            SaveConfigInternal();
            
            // Обновляем HUD для всех игроков
            foreach (var kvp in _userSettings)
            {
                var settings = kvp.Value;
                if (settings.IsEnabled)
                {
                    var playerObj = Utilities.GetPlayerFromSlot(settings.PlayerSlot);
                    if (playerObj != null && playerObj.IsValid && !playerObj.IsBot)
                    {
                        _hudInitialized[settings.PlayerSlot] = false;
                        Server.NextFrame(() => InitializeHUDForPlayer(playerObj));
                    }
                }
            }
            
            LogInfo(message);
        }
        else
        {
            command.ReplyToCommand("[SpeedometerHUD+WASD] Неверные значения. Использование: css_speedometer_setborder <ширина 0-10> <высота 0-10>");
            command.ReplyToCommand("ВАЖНО: Используйте точку (.) для десятичных чисел, например: css_speedometer_setborder 0.0 0.0");
        }
    }
    
    private void OnSetBoldCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (command.ArgCount < 2)
        {
            command.ReplyToCommand($"Использовать жирный шрифт: {Config.UseBoldFont} (по умолчанию: true)");
            command.ReplyToCommand($"Использование: css_speedometer_setbold <0/1>");
            return;
        }
        
        if (int.TryParse(command.GetArg(1), out int useBold))
        {
            bool oldValue = Config.UseBoldFont;
            Config.UseBoldFont = useBold == 1;
            
            string message = $"Жирный шрифт {(Config.UseBoldFont ? "включен" : "выключен")} (по умолчанию: включен)";
            command.ReplyToCommand($"[SpeedometerHUD+WASD] {message}");
            
            SaveConfigInternal();
            
            // Обновляем HUD для всех игроков
            foreach (var kvp in _userSettings)
            {
                var settings = kvp.Value;
                if (settings.IsEnabled)
                {
                    var playerObj = Utilities.GetPlayerFromSlot(settings.PlayerSlot);
                    if (playerObj != null && playerObj.IsValid && !playerObj.IsBot)
                    {
                        _hudInitialized[settings.PlayerSlot] = false;
                        Server.NextFrame(() => InitializeHUDForPlayer(playerObj));
                    }
                }
            }
            
            LogInfo(message);
        }
        else
        {
            command.ReplyToCommand("[SpeedometerHUD+WASD] Неверное значение. Использование: css_speedometer_setbold <0/1>");
        }
    }
    
    private void OnSetMethodCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (command.ArgCount < 2)
        {
            command.ReplyToCommand($"Текущий метод HUD: {Config.HudMethod} ({(Config.HudMethod == 1 ? "point_orient (стабильный)" : "teleport (быстрый)")}) (по умолчанию: 1)");
            command.ReplyToCommand($"Использование: css_speedometer_setmethod <0|1>");
            command.ReplyToCommand($"0 - телепорт (быстрее, но может трястись)");
            command.ReplyToCommand($"1 - point_orient (стабильнее, рекомендовано)");
            return;
        }

        if (int.TryParse(command.GetArg(1), out int method))
        {
            if (method == 0 || method == 1)
            {
                Config.HudMethod = method;
                SetGameHUDMethod(method == 1);
                
                string message = $"Метод HUD установлен: {(method == 1 ? "point_orient (стабильный)" : "teleport (быстрый)")} (по умолчанию: 1)";
                command.ReplyToCommand($"[SpeedometerHUD+WASD] {message}");
                
                SaveConfigInternal();
                
                // Переинициализируем HUD для всех игроков
                foreach (var kvp in _userSettings)
                {
                    var settings = kvp.Value;
                    if (settings.IsEnabled)
                    {
                        var playerObj = Utilities.GetPlayerFromSlot(settings.PlayerSlot);
                        if (playerObj != null && playerObj.IsValid && !playerObj.IsBot)
                        {
                            _hudInitialized[settings.PlayerSlot] = false;
                            Server.NextFrame(() => InitializeHUDForPlayer(playerObj));
                        }
                    }
                }
                
                LogInfo(message);
            }
            else
            {
                command.ReplyToCommand("[SpeedometerHUD+WASD] Неверное значение метода. Используйте 0 (телепорт) или 1 (point_orient)");
            }
        }
        else
        {
            command.ReplyToCommand("[SpeedometerHUD+WASD] Неверное значение метода. Используйте 0 (телепорт) или 1 (point_orient)");
        }
    }
    
    private void OnSetJumpTimeCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (command.ArgCount < 2)
        {
            command.ReplyToCommand($"Текущее время отображения прыжка: {Config.JumpDisplayTime}с (по умолчанию: 0.4)");
            command.ReplyToCommand($"Использование: css_speedometer_setjumptime <0.1-1.0>");
            command.ReplyToCommand($"Пример: css_speedometer_setjumptime 0.5");
            return;
        }
        
        // Получаем строку аргументов и парсим с использованием InvariantCulture
        string arg = command.GetArg(1).Replace(',', '.'); // Заменяем запятую на точку для совместимости
        
        if (float.TryParse(arg, NumberStyles.Float, CultureInfo.InvariantCulture, out float newTime))
        {
            newTime = Math.Clamp(newTime, 0.1f, 1.0f);
            float oldTime = Config.JumpDisplayTime;
            Config.JumpDisplayTime = newTime;
            
            string message = $"Время отображения прыжка изменено с {oldTime}с на {newTime}с (по умолчанию: 0.4)";
            command.ReplyToCommand($"[SpeedometerHUD+WASD] {message}");
            
            SaveConfigInternal();
            LogInfo(message);
        }
        else
        {
            command.ReplyToCommand("[SpeedometerHUD+WASD] Неверное значение времени. Использование: css_speedometer_setjumptime <0.1-1.0>");
            command.ReplyToCommand("ВАЖНО: Используйте точку (.) для десятичных чисел, например: 0.5");
        }
    }
    
    private void OnSetBhopWindowCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (command.ArgCount < 2)
        {
            command.ReplyToCommand($"Текущее окно распрыжки: {Config.BunnyhopWindow}с (по умолчанию: 0.5)");
            command.ReplyToCommand($"Использование: css_speedometer_setbhopwindow <0.1-1.0>");
            command.ReplyToCommand($"Пример: css_speedometer_setbhopwindow 0.5");
            return;
        }
        
        // Получаем строку аргументов и парсим с использованием InvariantCulture
        string arg = command.GetArg(1).Replace(',', '.'); // Заменяем запятую на точку для совместимости
        
        if (float.TryParse(arg, NumberStyles.Float, CultureInfo.InvariantCulture, out float newWindow))
        {
            newWindow = Math.Clamp(newWindow, 0.1f, 1.0f);
            float oldWindow = Config.BunnyhopWindow;
            Config.BunnyhopWindow = newWindow;
            
            string message = $"Окно распрыжки изменено с {oldWindow}с на {newWindow}с (по умолчанию: 0.5)";
            command.ReplyToCommand($"[SpeedometerHUD+WASD] {message}");
            
            SaveConfigInternal();
            LogInfo(message);
        }
        else
        {
            command.ReplyToCommand("[SpeedometerHUD+WASD] Неверное значение окна. Использование: css_speedometer_setbhopwindow <0.1-1.0>");
            command.ReplyToCommand("ВАЖНО: Используйте точку (.) для десятичных чисел, например: 0.5");
        }
    }
    
    private void OnSetLogLevelCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (command.ArgCount < 2)
        {
            command.ReplyToCommand($"Текущий уровень логирования: {Config.LogLevel} (по умолчанию: 0)");
            command.ReplyToCommand($"Использование: css_speedometer_setloglevel <0-2>");
            command.ReplyToCommand($"0 - только ошибки");
            command.ReplyToCommand($"1 - ошибки и информация");
            command.ReplyToCommand($"2 - ошибки, информация и отладка");
            return;
        }

        if (int.TryParse(command.GetArg(1), out int logLevel))
        {
            if (logLevel >= 0 && logLevel <= 2)
            {
                int oldLogLevel = Config.LogLevel;
                Config.LogLevel = logLevel;
                
                string message = $"Уровень логирования изменен с {oldLogLevel} на {logLevel} (по умолчанию: 0)";
                command.ReplyToCommand($"[SpeedometerHUD+WASD] {message}");
                
                SaveConfigInternal();
                LogInfo(message);
            }
            else
            {
                command.ReplyToCommand("[SpeedometerHUD+WASD] Неверный уровень логирования. Используйте значение от 0 до 2");
            }
        }
        else
        {
            command.ReplyToCommand("[SpeedometerHUD+WASD] Неверный уровень логирования. Используйте значение от 0 до 2");
        }
    }
    
    // Старые команды-псевдонимы для совместимости
    private void OnToggleKeysCommand(CCSPlayerController? player, CommandInfo command)
    {
        Config.ShowKeys = !Config.ShowKeys;
        
        string message = $"Отображение клавиш движения {(Config.ShowKeys ? "включено" : "выключено")} (по умолчанию: включено)";
        command.ReplyToCommand($"[SpeedometerHUD+WASD] {message}");
        
        SaveConfigInternal();
        LogInfo(message);
    }
    
    private void OnToggleSpectatorCommand(CCSPlayerController? player, CommandInfo command)
    {
        Config.ShowSpectator = !Config.ShowSpectator;
        
        string message = $"Отображение в режиме наблюдателя {(Config.ShowSpectator ? "включено" : "выключено")} (по умолчанию: включено)";
        command.ReplyToCommand($"[SpeedometerHUD+WASD] {message}");
        
        SaveConfigInternal();
        LogInfo(message);
        
        // Обновляем HUD для всех игроков при изменении этой настройки
        foreach (var kvp in _userSettings)
        {
            var settings = kvp.Value;
            if (settings.IsEnabled)
            {
                var playerObj = Utilities.GetPlayerFromSlot(settings.PlayerSlot);
                if (playerObj != null && playerObj.IsValid && !playerObj.IsBot)
                {
                    _hudInitialized[settings.PlayerSlot] = false;
                    Server.NextFrame(() => InitializeHUDForPlayer(playerObj));
                }
            }
        }
    }
    
    private void OnToggleBoldCommand(CCSPlayerController? player, CommandInfo command)
    {
        Config.UseBoldFont = !Config.UseBoldFont;
        
        string message = $"Жирный шрифт {(Config.UseBoldFont ? "включен" : "выключен")} (по умолчанию: включен)";
        command.ReplyToCommand($"[SpeedometerHUD+WASD] {message}");
        
        SaveConfigInternal();
        
        // Обновляем HUD для всех игроков
        foreach (var kvp in _userSettings)
        {
            var settings = kvp.Value;
            if (settings.IsEnabled)
            {
                var playerObj = Utilities.GetPlayerFromSlot(settings.PlayerSlot);
                if (playerObj != null && playerObj.IsValid && !playerObj.IsBot)
                {
                    _hudInitialized[settings.PlayerSlot] = false;
                    Server.NextFrame(() => InitializeHUDForPlayer(playerObj));
                }
            }
        }
        
        LogInfo(message);
    }
    
    private void OnTestCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (player == null)
        {
            command.ReplyToCommand("[SpeedometerHUD+WASD] Эта команда может использоваться только игроками");
            return;
        }
        
        if (!Config.Enabled)
        {
            command.ReplyToCommand("[SpeedometerHUD+WASD] Плагин выключен. Включите его командой css_speedometer_enable");
            return;
        }
        
        if (_hudapi == null)
        {
            command.ReplyToCommand("[SpeedometerHUD+WASD] GameHUD API не загружена. Проверьте установку плагина GameHUD");
            return;
        }
        
        try
        {
            // Показываем тестовое сообщение
            _hudapi.Native_GameHUD_Show(player, (byte)Config.HudChannel, "TEST\nSPEED: 250\nW  A  S  D", 5.0f);
            
            command.ReplyToCommand($"[SpeedometerHUD+WASD] Тестовое сообщение отправлено на канал {Config.HudChannel} на 5 секунд");
            LogInfo($"Тестовое сообщение отправлено игроку {player.PlayerName}");
        }
        catch (Exception ex)
        {
            command.ReplyToCommand($"[SpeedometerHUD+WASD] Ошибка отправки тестового сообщения: {ex.Message}");
            LogError($"Ошибка тестовой команды для {player.PlayerName}: {ex.Message}");
        }
    }
    
    private void OnEnableCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (Config.Enabled)
        {
            command.ReplyToCommand("[SpeedometerHUD+WASD] Плагин уже включен (по умолчанию: включен)");
            return;
        }
        
        Config.Enabled = true;
        
        string message = "Плагин включен (по умолчанию: включен)";
        command.ReplyToCommand($"[SpeedometerHUD+WASD] {message}");
        
        SaveConfigInternal();
        
        if (_updateTimer == null)
        {
            _updateTimer = AddTimer(Config.UpdateInterval, UpdateSpeedometer, 
                CounterStrikeSharp.API.Modules.Timers.TimerFlags.REPEAT);
        }
        
        // Инициализируем HUD для всех игроков (только не ботов)
        foreach (var playerObj in Utilities.GetPlayers())
        {
            if (playerObj != null && playerObj.IsValid && !playerObj.IsBot && !playerObj.IsHLTV)
            {
                int slot = playerObj.Slot;
                if (!_userSettings.ContainsKey(slot))
                {
                    InitializePlayer(playerObj);
                }
                else if (_userSettings[slot].IsEnabled)
                {
                    _hudInitialized[slot] = false;
                    Server.NextFrame(() => InitializeHUDForPlayer(playerObj));
                }
            }
        }
        
        LogInfo(message);
    }
    
    private void OnDisableCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (!Config.Enabled)
        {
            command.ReplyToCommand("[SpeedometerHUD+WASD] Плагин уже выключен");
            return;
        }
        
        Config.Enabled = false;
        
        string message = "Плагин выключен";
        command.ReplyToCommand($"[SpeedometerHUD+WASD] {message}");
        
        SaveConfigInternal();
        
        if (_updateTimer != null)
        {
            _updateTimer.Kill();
            _updateTimer = null;
        }
        
        // Удаляем HUD у всех игроков
        if (_hudapi != null)
        {
            foreach (var playerObj in Utilities.GetPlayers())
            {
                if (playerObj != null && playerObj.IsValid)
                {
                    _hudapi.Native_GameHUD_Remove(playerObj, (byte)Config.HudChannel);
                }
            }
        }
        
        LogInfo(message);
    }
    
    private void SaveConfigInternal()
    {
        try
        {
            var configsPath = Path.Combine(Server.GameDirectory, "counterstrikesharp", "configs", "plugins", "CS2_SpeedometerHUD_WASD");
            Directory.CreateDirectory(configsPath);
            var configFilePath = Path.Combine(configsPath, "CS2_SpeedometerHUD_WASD.json");
            
            var jsonOptions = new System.Text.Json.JsonSerializerOptions 
            { 
                WriteIndented = true, 
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping 
            };
            string json = System.Text.Json.JsonSerializer.Serialize(Config, jsonOptions);
            File.WriteAllText(configFilePath, json);
            
            LogDebug($"Конфиг сохранен в: {configFilePath}");
        }
        catch (Exception ex)
        {
            LogError($"Ошибка сохранения конфига: {ex.Message}");
        }
    }
    
    public override void Unload(bool hotReload)
    {
        if (_updateTimer != null)
        {
            _updateTimer.Kill();
            _updateTimer = null;
        }
        
        if (_hudapi != null)
        {
            foreach (var player in Utilities.GetPlayers())
            {
                if (player != null && player.IsValid)
                {
                    _hudapi.Native_GameHUD_Remove(player, (byte)Config.HudChannel);
                }
            }
        }
        
        _userSettings.Clear();
        _hudInitialized.Clear();
        _playerActive.Clear();
        _previousJumpButton.Clear();
        _lastJumpPress.Clear();
        _lastJumpEvent.Clear();
        _wasOnGround.Clear();
        
        LogInfo("Плагин выгружен");
    }
    
    private void LogError(string message)
    {
        Console.WriteLine($"[SpeedometerHUD+WASD ERROR] {DateTime.Now:HH:mm:ss} - {message}");
    }
    
    private void LogInfo(string message)
    {
        if (Config.LogLevel >= 1)
            Console.WriteLine($"[SpeedometerHUD+WASD INFO] {DateTime.Now:HH:mm:ss} - {message}");
    }
    
    private void LogWarning(string message)
    {
        if (Config.LogLevel >= 0)
            Console.WriteLine($"[SpeedometerHUD+WASD WARNING] {DateTime.Now:HH:mm:ss} - {message}");
    }
    
    private void LogDebug(string message)
    {
        if (Config.LogLevel >= 2)
            Console.WriteLine($"[SpeedometerHUD+WASD DEBUG] {DateTime.Now:HH:mm:ss} - {message}");
    }
}

public class UserSettings
{
    public int PlayerSlot { get; set; }
    public bool IsEnabled { get; set; } = true;
}