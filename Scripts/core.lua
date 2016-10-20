function print(x) cs:print(tostring(x)) end

Color = 
{
  alpha = function(clr, a) return { clr[1], clr[2], clr[3], a } end,

  AliceBlue = { 240, 248, 255 },
  AntiqueWhite = { 250, 235, 215 },
  Aqua = { 0, 255, 255 },
  Aquamarine = { 127, 255, 212 },
  Azure = { 240, 255, 255 },
  Beige = { 245, 245, 220 },
  Bisque = { 255, 228, 196 },
  Black = { 0, 0, 0 },
  BlanchedAlmond = { 255, 235, 205 },
  Blue = { 0, 0, 255 },
  BlueViolet = { 138, 43, 226 },
  Brown = { 165, 42, 42 },
  BurlyWood = { 222, 184, 135 },
  CadetBlue = { 95, 158, 160 },
  Chartreuse = { 127, 255, 0 },
  Chocolate = { 210, 105, 30 },
  Coral = { 255, 127, 80 },
  CornflowerBlue = { 100, 149, 237 },
  Cornsilk = { 255, 248, 220 },
  Crimson = { 220, 20, 60 },
  Cyan = { 0, 255, 255 },
  DarkBlue = { 0, 0, 139 },
  DarkCyan = { 0, 139, 139 },
  DarkGoldenrod = { 184, 134, 11 },
  DarkGray = { 169, 169, 169 },
  DarkGreen = { 0, 100, 0 },
  DarkKhaki = { 189, 183, 107 },
  DarkMagenta = { 139, 0, 139 },
  DarkOliveGreen = { 85, 107, 47 },
  DarkOrange = { 255, 140, 0 },
  DarkOrchid = { 153, 50, 204 },
  DarkRed = { 139, 0, 0 },
  DarkSalmon = { 233, 150, 122 },
  DarkSeaGreen = { 143, 188, 139 },
  DarkSlateBlue = { 72, 61, 139 },
  DarkSlateGray = { 47, 79, 79 },
  DarkTurquoise = { 0, 206, 209 },
  DarkViolet = { 148, 0, 211 },
  DeepPink = { 255, 20, 147 },
  DeepSkyBlue = { 0, 191, 255 },
  DimGray = { 105, 105, 105 },
  DodgerBlue = { 30, 144, 255 },
  Firebrick = { 178, 34, 34 },
  FloralWhite = { 255, 250, 240 },
  ForestGreen = { 34, 139, 34 },
  Fuchsia = { 255, 0, 255 },
  Gainsboro = { 220, 220, 220 },
  GhostWhite = { 248, 248, 255 },
  Gold = { 255, 215, 0 },
  Goldenrod = { 218, 165, 32 },
  Gray = { 128, 128, 128 },
  Green = { 0, 128, 0 },
  GreenYellow = { 173, 255, 47 },
  Honeydew = { 240, 255, 240 },
  HotPink = { 255, 105, 180 },
  IndianRed = { 205, 92, 92 },
  Indigo = { 75, 0, 130 },
  Ivory = { 255, 255, 240 },
  Khaki = { 240, 230, 140 },
  Lavender = { 230, 230, 250 },
  LavenderBlush = { 255, 240, 245 },
  LawnGreen = { 124, 252, 0 },
  LemonChiffon = { 255, 250, 205 },
  LightBlue = { 173, 216, 230 },
  LightCoral = { 240, 128, 128 },
  LightCyan = { 224, 255, 255 },
  LightGoldenrodYellow = { 250, 250, 210 },
  LightGray = { 211, 211, 211 },
  LightGreen = { 144, 238, 144 },
  LightPink = { 255, 182, 193 },
  LightSalmon = { 255, 160, 122 },
  LightSeaGreen = { 32, 178, 170 },
  LightSkyBlue = { 135, 206, 250 },
  LightSlateGray = { 119, 136, 153 },
  LightSteelBlue = { 176, 196, 222 },
  LightYellow = { 255, 255, 224 },
  Lime = { 0, 255, 0 },
  LimeGreen = { 50, 205, 50 },
  Linen = { 250, 240, 230 },
  Magenta = { 255, 0, 255 },
  Maroon = { 128, 0, 0 },
  MediumAquamarine = { 102, 205, 170 },
  MediumBlue = { 0, 0, 205 },
  MediumOrchid = { 186, 85, 211 },
  MediumPurple = { 147, 112, 219 },
  MediumSeaGreen = { 60, 179, 113 },
  MediumSlateBlue = { 123, 104, 238 },
  MediumSpringGreen = { 0, 250, 154 },
  MediumTurquoise = { 72, 209, 204 },
  MediumVioletRed = { 199, 21, 133 },
  MidnightBlue = { 25, 25, 112 },
  MintCream = { 245, 255, 250 },
  MistyRose = { 255, 228, 225 },
  Moccasin = { 255, 228, 181 },
  NavajoWhite = { 255, 222, 173 },
  Navy = { 0, 0, 128 },
  OldLace = { 253, 245, 230 },
  Olive = { 128, 128, 0 },
  OliveDrab = { 107, 142, 35 },
  Orange = { 255, 165, 0 },
  OrangeRed = { 255, 69, 0 },
  Orchid = { 218, 112, 214 },
  PaleGoldenrod = { 238, 232, 170 },
  PaleGreen = { 152, 251, 152 },
  PaleTurquoise = { 175, 238, 238 },
  PaleVioletRed = { 219, 112, 147 },
  PapayaWhip = { 255, 239, 213 },
  PeachPuff = { 255, 218, 185 },
  Peru = { 205, 133, 63 },
  Pink = { 255, 192, 203 },
  Plum = { 221, 160, 221 },
  PowderBlue = { 176, 224, 230 },
  Purple = { 128, 0, 128 },
  Red = { 255, 0, 0 },
  RosyBrown = { 188, 143, 143 },
  RoyalBlue = { 65, 105, 225 },
  SaddleBrown = { 139, 69, 19 },
  Salmon = { 250, 128, 114 },
  SandyBrown = { 244, 164, 96 },
  SeaGreen = { 46, 139, 87 },
  SeaShell = { 255, 245, 238 },
  Sienna = { 160, 82, 45 },
  Silver = { 192, 192, 192 },
  SkyBlue = { 135, 206, 235 },
  SlateBlue = { 106, 90, 205 },
  SlateGray = { 112, 128, 144 },
  Snow = { 255, 250, 250 },
  SpringGreen = { 0, 255, 127 },
  SteelBlue = { 70, 130, 180 },
  Tan = { 210, 180, 140 },
  Teal = { 0, 128, 128 },
  Thistle = { 216, 191, 216 },
  Tomato = { 255, 99, 71 },
  Turquoise = { 64, 224, 208 },
  Violet = { 238, 130, 238 },
  Wheat = { 245, 222, 179 },
  White = { 255, 255, 255 },
  WhiteSmoke = { 245, 245, 245 },
  Yellow = { 255, 255, 0 },
  YellowGreen = { 154, 205, 50 },
}

Part =
{
  name = "Part",
  type = "Part",

  form = "rectangle",
}

Part.Defs =
{
  Part = Part
}

Part.Colors =
{
  Board = Color.DarkGreen,
  ViaHole = Color.Black,
  ViaRing = Color.DarkGoldenrod,
  Track = Color.Gray,
  Wire = { 32, 32, 32 },
  Pin = Color.DarkGoldenrod,
  Leg = Color.Silver,
  Component = Color.alpha(Color.White, 128),
  ComponentFrame = Color.Black,
}

Part.ConnectionColors =
{
  { "Black", Part.Colors.Wire },
  { "Red", Color.DarkRed },
  { "Blue", Color.DarkBlue },
  { "Green", { 0, 192, 0 } },
  { "Yellow", { 224, 224, 0 } },
  { "Orange", { 255, 165, 0 } },
  { "Purple", { 128, 0, 128 } },
  { "Brown", { 139, 69, 19 } },
  { "Gray",  { 96, 96, 96 } },
  { "White", { 224, 224, 224 } },
}

function copy_table(tbl)
  if not tbl then return nil end
  local res = {}
  for k,v in pairs(tbl) do res[k] = v end
  return res
end

function IsKindOf(tbl, base)
  while true do
    if tbl == base then return true end
    if not tbl then return false end
    local mt = getmetatable(tbl)
    if not mt then return false end
    tbl = mt.__index
  end
end

function inherit(tbl, base, is_def)
  if IsKindOf(tbl, base) then return end
  if getmetatable(tbl) then error("inherit: metatable already set") end
  if not rawget(base, "__mt_base") then base.__mt_base = { __index = base } end
  setmetatable(tbl, base.__mt_base)
  tbl.__base = base
  if is_def then
    if not rawget(base, "__derived") then base.__derived = {} end
    table.insert(base.__derived, tbl) 
  end
end

function modify(b1, b2, b3)
  local bases = {}
  if b1 then table.insert(bases, b1) end
  if b2 then table.insert(bases, b2) end
  if b3 then table.insert(bases, b3) end
  return function(tbl)
    if #bases then
      setmetatable(tbl, { __index = function(t, k)
        for _,base in ipairs(bases) do
          local v = base[k]
          if v ~= nil then return v end
        end
        return nil
      end });
    end
    return tbl
  end
end

function DefPart(name, base_name)
  if Part.Defs[name] then error("Part '" .. name .. "' is already defined") end
  local base = Part.Defs[base_name]
  if not base then error("Part '" .. name .. "' base '" .. base_name .. "' is not defined") end
  return function(def)
    def.name = name
    inherit(def, base, true)
    Part.Defs[name] = def
  end
end

function Part:InitParent(def)
  if not def then error("def not specified in " .. tostring(self.name) .. ":InitParent()") end
  local base = def.__base
  while true do
    if not base then return end
    local Init = rawget(base, "Init")
    if Init then
      Init(self, base)
      return
    end
    base = base.__base
  end
end

function Part:AddTrack(x1, y1, x2, y2, def)
  if not rawget(self, "tracks") then self.tracks = {} end
  local track = modify(def, self.track_def, Part.Defs.Track) {
    x1 = x1,
    y1 = y1,
    x2 = x2,
    y2 = y2,
  }
  table.insert(self.tracks, track)
end

function Part:AddWire(x1, y1, x2, y2, def)
  if not rawget(self, "wires") then self.wires = {} end
  local wire = modify(def, self.wire_def, Part.Defs.Wire) {
    x1 = x1,
    y1 = y1,
    x2 = x2,
    y2 = y2,
  }
  table.insert(self.wires, wire)
end

function Part:AddVia(x, y, name, def)
  if not rawget(self, "vias") then self.vias = {} end
  local via = modify(def, self.via_def, Part.Defs.Via) {
    x = x,
    y = y,
    name = name,
  }
  table.insert(self.vias, via)
end

function Part:AddPin(x, y, name, def)
  if not rawget(self, "pins") then self.pins = {} end
  local pin = modify(def, self.pin_def, Part.Defs.Pin) {
    x = x,
    y = y,
    name = name,
  }
  table.insert(self.pins, pin)
end

function Part:AddToChildBounds(x, y, r)
  if x - r < self.child_bounds.min_x then self.child_bounds.min_x = x - r end
  if y - r < self.child_bounds.min_y then self.child_bounds.min_y = y - r end
  if x + r > self.child_bounds.max_x then self.child_bounds.max_x = x + r end
  if y + r > self.child_bounds.max_y then self.child_bounds.max_y = y + r end
end

function Part:CalcBounds()
  if rawget(self, "bounds") then return end
  
  if not rawget(self, "child_bounds") then
    self.child_bounds = { min_x = 0, min_y = 0, max_x = 0, max_y = 0 }
  end

  local vias = rawget(self, "vias")
  if vias then
    for i,via in ipairs(vias) do
      self:AddToChildBounds(via.x, via.y, via.outer_radius)
    end
  end

  local tracks = rawget(self, "tracks")
  if tracks then
    for i,track in ipairs(tracks) do
      local min_x = math.min(track.x1, track.x2)
      local min_y = math.min(track.y1, track.y2)
      local max_x = math.max(track.x1, track.x2)
      local max_y = math.max(track.y1, track.y2)
      self:AddToChildBounds(min_x, min_y, track.width / 2)
      self:AddToChildBounds(max_x, max_y, track.width / 2)
    end
  end

  local pins = rawget(self, "pins")
  if pins then
    for i,pin in ipairs(pins) do
    self:AddToChildBounds(pin.x, pin.y, pin.radius)
    end
  end

  local g = self.grow_bounds or 0
  self.bounds = {
    min_x = self.child_bounds.min_x - g,
    min_y = self.child_bounds.min_y - g,
    max_x = self.child_bounds.max_x + g,
    max_y = self.child_bounds.max_y + g
  }

  if self.form == "square" or self.form == "circle" then
    local w = self.bounds.max_x - self.bounds.min_x
    local h = self.bounds.max_y - self.bounds.min_y
    if w >= h then
      self.bounds.min_y = self.bounds.min_y - (w - h) / 2
      self.bounds.max_y = self.bounds.max_y + (w - h) / 2
    else
      self.bounds.min_x = self.bounds.min_x - (h - w) / 2
      self.bounds.max_x = self.bounds.max_x + (h - w) / 2
    end
  elseif self.form == "D-up" then
    local w = self.bounds.max_x - self.bounds.min_x
    self.bounds.min_y = self.bounds.max_y - w / 2        
  end
end

--------------------------------- Boards

DefPart("Board", "Part")
{
  type = "Board",
  z_order = 1,

  grow_bounds = 1.0,
  color = Part.Colors.Board,
}

DefPart("Blank board", "Board")
{
  bounds = {
    min_x = 0,
    min_y = 0,
    max_x = 50,
    max_y = 50,
  }  
}

DefPart("Proto board", "Board")
{
  columns = 24,
  rows = 16,
  
  raster = 2.54,

  track_def = {
    width = 2,
  },

  AddVias = function(self, x1, y1, x2, y2)
    for row = y1, y2 do
      local y = (row - 1) * self.raster;
      for col = x1, x2 do
        local x = (col - 1) * self.raster; 
        self:AddVia(x, y, "Via " .. col .. "," .. row)
      end 
    end
  end,

  InitVias = function(self)
    self:AddVias(1, 1, self.columns, self.rows)
  end,

  InitTracks = function(self)
    local max_x = (self.columns - 1) * self.raster
    local max_y = (self.rows - 1) * self.raster
    self:AddTrack(0, 0, max_x, 0)
    self:AddTrack(0, max_y, max_x, max_y)
  end,

  Init = function(self, def)
    self:InitVias()
    self:InitTracks()
  end,
}

DefPart("Proto board (Big)", "Proto board")
{
  columns = 48,
  rows = 32,
}

DefPart("Proto 2mm", "Proto board")
{
  raster = 2,
  via_def = 
  {
    --hole_radius = 0.2,
    --outer_radius = 0.3,
    form = "square",
  },
  track_def = 
  {
    --width = 0.8,
    --color = Part.Colors.Track,
  }
}

DefPart("Strip board (Vertical)", "Proto board")
{
  columns = 24,
  rows = 16,

  InitTracks = function(self)
    local max_y = (self.rows - 1) * self.raster
    for col = 1, self.columns do
      local x = (col - 1) * self.raster
      self:AddTrack(x, 0, x, max_y)
    end
  end,
}

DefPart("Strip board (Horizontal)", "Proto board")
{
  columns = 24,
  rows = 16,

  InitTracks = function(self)
    local max_x = (self.columns - 1) * self.raster
    for row = 1, self.rows do
      local y = (row - 1) * self.raster
      self:AddTrack(0, y, max_x, y)
    end
  end,
}

DefPart("TPetrov 1", "Proto board")
{
  columns = 45,
  rows = 18,

  DV = function(self, x, y)
    x = x * self.raster
    y = y * self.raster
    for i,via in ipairs(self.vias) do
      if via.x == x and via.y == y then
        table.remove(self.vias, i)
        return
      end
    end
  end,

  AT = function(self, x1, y1, x2, y2)
    local r = self.raster
    if x1 < 0 then x1 = self.columns + x1 end
    if y1 < 0 then y1 = self.rows + y1 end
    if x2 < 0 then x2 = self.columns + x2 end
    if y2 < 0 then y2 = self.rows + y2 end
    self:AddTrack(x1 * r, y1 * r, x2 * r, y2 * r)
  end,

  InitVias = function(self)
    Part.Defs["Proto board"].InitVias(self)
    for x = 0,1 do
      for y = 0, 1 do
        self:DV(x, y)
        self:DV(x, self.rows - 1 - y)
      end
    end
  end,

  InitTracks = function(self)
    self:AT(2, 0, -1, 0)
    self:AT(4, 1, -3, 1)
    self:AT(4, 2, -3, 2)

    self:AT(2, -1, -1, -1)
    self:AT(4, -2, -3, -2)
    self:AT(4, -3, -3, -3)

    self:AT(0, 2, 0, 7)
    self:AT(1, 2, 1, 7)
    self:AT(0, 10, 0, 15)
    self:AT(1, 10, 1, 15)

    self:AT(2, 1, 2, 7)
    self:AT(3, 1, 3, 7)
    self:AT(2, 10, 2, 16)
    self:AT(3, 10, 3, 16)

    self:AT(-2, 1, -2, 7)
    self:AT(-1, 1, -1, 7)
    self:AT(-2, 10, -2, 16)
    self:AT(-1, 10, -1, 16)

    for x = 4, self.columns - 3 do
      self:AT(x, 3, x, 7)
      self:AT(x, 10, x, 14)
    end

    for x = 0, self.columns - 1 do
      self:AT(x, 8, x, 9)
    end
  end,
}

DefPart("TPetrov Small", "Proto board")
{
  columns = 18,
  rows = 10,

  via_def = { color_ring = Part.Colors.Track },

  InitVias = function(self)
      for row = 1, self.rows do
      local y = (row - 1) * self.raster
      for col = 1, self.columns do
        local x = (col - 1) * self.raster
        if row == 1 or row == self.rows or col < 9 or col > 10 then
          local form = nil
          if row == 1 and col > 1 and col < self.columns then
            form = "square"
          end
          self:AddVia(x, y, "Via " .. col .. "," .. row, { form = form })
        end
      end
    end
  end,

  InitTracks = function(self)
    self:AddTrack(0, 0, 0, (self.rows - 1) * self.raster)
    self:AddTrack((self.columns - 1) * self.raster, 0, (self.columns - 1) * self.raster, (self.rows - 1) * self.raster)
    self:AddTrack(0, (self.rows - 1) * self.raster, (self.columns - 1) * self.raster, (self.rows - 1) * self.raster)
    for row = 2, self.rows - 1 do
      local y = (row - 1) * self.raster
      self:AddTrack(1 * self.raster, y, 2 * self.raster, y)
      self:AddTrack(3 * self.raster, y, 5 * self.raster, y)
      self:AddTrack(6 * self.raster, y, 7 * self.raster, y)

      self:AddTrack(10 * self.raster, y, 11 * self.raster, y)
      self:AddTrack(12 * self.raster, y, 14 * self.raster, y)
      self:AddTrack(15 * self.raster, y, 16 * self.raster, y)
    end
  end,
}

DefPart("TPetrov Small 2", "TPetrov Small")
{
  InitTracks = function(self)
    self:AddTrack(0, 0, 0, (self.rows - 2) * self.raster)
    self:AddTrack((self.columns - 1) * self.raster, 0, (self.columns - 1) * self.raster, (self.rows - 2) * self.raster)
    self:AddTrack(self.raster, (self.rows - 1) * self.raster, (self.columns - 2) * self.raster, (self.rows - 1) * self.raster)
    for row = 2, self.rows - 1 do
      local y = (row - 1) * self.raster
      self:AddTrack(1 * self.raster, y, 2 * self.raster, y)
      self:AddTrack(3 * self.raster, y, 5 * self.raster, y)
      self:AddTrack(6 * self.raster, y, 7 * self.raster, y)

      self:AddTrack(10 * self.raster, y, 11 * self.raster, y)
      self:AddTrack(12 * self.raster, y, 14 * self.raster, y)
      self:AddTrack(15 * self.raster, y, 16 * self.raster, y)
    end
  end,
}

DefPart("Breadboard", "Board")
{
  columns = 30,
  raster = 2.54,

  power_lines = true,

  color = Color.Gray,

  AddPowerLine = function(self, y, name, color)
    local g = math.floor(self.columns / 6)
    local x = (self.columns - (g * 6 - 1)) * self.raster / 2
    local idx = 1
    while x + 5 * self.raster < self.columns * self.raster do
      for i = 1,5 do
        self:AddVia(x + (i - 1) * self.raster, y, "Hole " .. idx .. "," .. name, { color_ring = self.color })
        idx = idx + 1
      end
      x = x + 6 * self.raster
    end
    self:AddTrack(0, y, (self.columns - 1) * self.raster, y, { name = name, color = color }) 
  end,

  InitPowerLines = function(self)
    if not self.power_lines then return end
    self:AddPowerLine(0, "Top GND", Color.DarkBlue)
    self:AddPowerLine(self.raster, "Top +", Color.DarkRed)
    self:AddPowerLine(15 * self.raster, "Bottom GND", Color.DarkBlue)
    self:AddPowerLine(16 * self.raster, "Bottom +", Color.DarkRed)
  end,

  InitVias = function(self, row_ofs)
    local row_names = { { "A", "B", "C", "D", "E" },  { "F", "G", "H", "I", "J" } }
    for col = 1, self.columns do
      local x = (col - 1) * self.raster
      for row = 1, 5 do
        local y = (row_ofs + row - 1) * self.raster
        self:AddVia(x, y, "Hole " .. col .. row_names[1][row], { color_ring = self.color })
        y = (row_ofs + row + 5) * self.raster
        self:AddVia(x, y, "Hole " .. col .. row_names[2][row], { color_ring = self.color})
      end
      self:AddTrack(x, row_ofs * self.raster, x, (row_ofs + 4) * self.raster)
      self:AddTrack(x, (row_ofs + 6 ) * self.raster, x, (row_ofs + 10) * self.raster)
    end
  end,

  Init = function(self, def)    
    self:InitPowerLines()
    self:InitVias(self.power_lines and 3 or 0)
  end,
}

DefPart("Breadboard (Small)", "Breadboard")
{
  columns = 17,
  power_lines = false,
}

DefPart("Breadboard (Big)", "Breadboard")
{
  columns = 63,
}


--------------------------------- Connections

DefPart("Connection", "Part")
{
  type = "Connection",
}

DefPart("Via", "Connection")
{
  type = "Via",
  z_order = 3,

  hole_radius = 0.45,
  outer_radius = 0.8,
  form = "circle",
  color_hole = Part.Colors.ViaHole,
  color_ring = Part.Colors.ViaRing,

  Init = function(self, def)
    self:AddVia(0, 0)
  end
}

DefPart("Track", "Connection")
{
  z_order = 2,
  connection_type = "Track",
  width = 1.0,
  color = Part.Colors.Track,
}

DefPart("Wire", "Connection")
{
  z_order = 4,
  connection_type = "Wire",
  width = 1.0,
  color = Part.Colors.Wire,
}

DefPart("Wire (Black)", "Wire")
{
  color = Part.Colors.Wire,
}

DefPart("Wire (Red)", "Wire")
{
  color = Color.DarkRed,
}

DefPart("Wire (Blue)", "Wire")
{
  color = Color.DarkBlue,
}

DefPart("Wire (Green)", "Wire")
{
  color = { 0, 192, 0 },
}

DefPart("Wire (Yellow)", "Wire")
{
  color = { 224, 224, 0 },
}

DefPart("Wire (Orange)", "Wire")
{
  color = { 255, 165, 0 },
}

DefPart("Wire (Purple)", "Wire")
{
  color = { 128, 0, 128 },
}

DefPart("Wire (Brown)", "Wire")
{
  color = { 139, 69, 19 },
}

DefPart("Wire (Gray)", "Wire")
{
  color = { 96, 96, 96 },
}


DefPart("Wire (White)", "Wire")
{
  color = { 224, 224, 224 },
}

DefPart("Pad", "Connection")
{
  type = "Pad",
  z_order = 3,
}

DefPart("Pin", "Connection")
{
  type = "Pin",
  z_order = 6,
  radius = 0.4,
  form = "circle",
  color = Part.Colors.Pin,
}

DefPart("Leg", "Connection")
{
  connection_type = "Wire",
  z_order = 4,
  width = 0.8,
  color = Part.Colors.Leg,
}

--------------------------------- Components

DefPart("Component", "Part")
{
  type = "Component",
  z_order = 5,
  color = Part.Colors.Component,
  frame_color = Part.Colors.ComponentFrame,
}

DefPart("Through Hole", "Component")
{
  pin_spacing = 2.54,
  pin_columns = 2,
  pin_rows = 1,
  row_spacing = 1,
  grow_bounds = 0.6,

  Init = function(self, def) 
    local i = 1 
    local left_to_right = true
    for row = self.pin_rows, 1, -1 do
      local c1, c2, step
      if left_to_right then
        c1, c2, step = 1, self.pin_columns, 1
      else
        c1, c2, step = self.pin_columns, 1, -1
      end
      left_to_right = not left_to_right
      for col = c1, c2, step do
        local x = (col - 1) * self.pin_spacing; 
        local y = (row - 1) * (self.row_spacing + 1) * self.pin_spacing;
        local pin = nil
        if i == 1 then pin = modify(self.pin_def) { form = "square" } end 
        if self.pin_rows == 1 and self.pin_columns == 2 and (def and def.bendable or self.pin_def and self.pin_def.bendable) and (col == c1 or col == c2) then
          if not pin then pin = {} end
          pin.bendable_ofs = { x = (col == 1) and -self.pin_spacing or self.pin_spacing, y = 0 }
        end
        self:AddPin(x, y, self.pin_names and self.pin_names[i] or ("Pin " .. i), pin)
        i = i + 1
      end 
    end
  end,
}

DefPart("Bendable", "Through Hole")
{
  pin_def = { bendable = true }
}

DefPart("Resistor (TH)", "Bendable")
{
  form = "capsule",
  color = { 241, 163, 64, 128 },
  value = 100,
  value_unit = "K{ohm}",

  value_name = "Resistance",
  value_units = "m{ohm}|{ohm}|K{ohm}|M{ohm}",
  values = "0 {ohm}|47 {ohm}|100 {ohm}|150 {ohm}|220 {ohm}|330 {ohm}|470 {ohm}|1 K{ohm}|10 K{ohm}|20 K{ohm}|100 K{ohm}|1 M{ohm}",
}

DefPart("Capaitor (TH)", "Bendable")
{
  color =  { 215, 100, 0, 192 },
  form = "ellipse",
  value = 1,
  value_unit = "{micro}F",

  value_name = "Capacitance",
  value_units = "pF|nF|{micro}F|F",
  values = "0.01|0.1|1|10|47|100|470|1000|2000",
}

DefPart("Electrolytic Capaitor (TH)", "Capaitor (TH)")
{
  color =  { 32, 32, 32, 192 },
  form = "circle",
  pin_names = 
  {
    "Cathode (-)",
    "Anode (+)",
  }
}

DefPart("Push button, SPST (TH)", "Bendable")
{
  color =  { 32, 32, 32, 192 },
  label = "Button",
}

DefPart("LED (TH)", "Bendable")
{
  form = "circle",
  color = { 255, 0, 0, 128 },

  pin_names = 
  {
    "Cathode (-)",
    "Anode (+)",
  }
}

DefPart("Transistor (TH)", "Bendable")
{
  form = "D-up",
  color =  { 64, 64, 64, 128 },
  pin_columns = 3,
  pin_names =
  {
    "Emmiter",
    "Base",
    "Collector",
  }
}

DefPart("NPN BJT (TH)", "Transistor (TH)")
{
}

DefPart("PNP BJT (TH)", "Transistor (TH)")
{
}

DefPart("N-channel MOSFET (TH)", "Transistor (TH)")
{
  pin_names =
  {
    "Drain",
    "Gate",
    "Source",
  }
}

DefPart("BS170", "N-channel MOSFET (TH)")
{
}

DefPart("IRF530", "N-channel MOSFET (TH)")
{
  form = "rectangle",
  pin_names =
  {
    "Gate",
    "Drain",
    "Source",
  }
}

DefPart("P-channel MOSFET (TH)", "Transistor (TH)")
{
  pin_names =
  {
    "Drain",
    "Gate",
    "Source",
  }
}

DefPart("IRF5305", "P-channel MOSFET (TH)")
{
  form = "rectangle",
  pin_names =
  {
    "Gate",
    "Drain",
    "Source",
  }
}

DefPart("DIP", "Through Hole")
{
  color =  { 64, 64, 64, 128 },
  pin_rows = 2,
}

DefPart("DIP4", "DIP")
{
  pin_columns = 2,
}

DefPart("DIP6", "DIP")
{
  pin_columns = 3,
}

DefPart("DIP8", "DIP")
{
  pin_columns = 4,
}

DefPart("ESP-01", "DIP8")
{
  color = { 0, 0, 128, 128 },

  row_spacing = 0,
  bounds = 
  {
    min_x = -3.2,
    min_y = -20.5,
    max_x = 11,
    max_y = 4.5,
  },
  
  pin_names = 
  {
    "TX/GPIO1",
    "CH_EN",
    "RESET",
    "VCC",

    "RX/GPIO3",
    "GPIO0",
    "GPIO2",
    "GND",
  }
}

DefPart("DIP10", "DIP")
{
  pin_columns = 5,
}

DefPart("555 Timer (DIP)", "DIP8")
{
  pin_names =
  {
    "GND",
    "TRIGGER",
    "OUTPUT",
    "RESET",

    "CTRL. VOLTAGE",
    "THRESHOLD",
    "DISCHARGE",
    "VCC",
  }
}

--------------------------------

function EnumParts(func, part, depth)
  if not part then part = Part end
  if not depth then depth = 0 end
  func(part, depth)
  local derived = rawget(part, "__derived")
  if derived then
  for i,p in ipairs(derived) do
    EnumParts(func, p, depth + 1)
  end
  end
end

function InitPart(part, depth)
  local def = part
  while def and not rawget(def, "Init") do def = def.__base end
  if def then
    def.InitParent(part, def)
	  def.Init(part, def)
  end
  part:CalcBounds()

  for _,k in ipairs{ "tracks", "vias", "pins" } do
    if part[k] and not rawget(part, k) then
      part[k] = {}
    end
  end

  cs:AddPart(part)
end

function InitParts()
  EnumParts(InitPart)
end


--------------------------------------------

function SelectedDocumentChanged()
  doc = frm and frm.doc
  sel = doc and doc.SelectedParts
end