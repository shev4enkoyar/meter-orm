namespace MeterOrm.Dlms.Enums;

public enum DlmsClassId
{
    /// <summary>
    ///     Data
    /// </summary>
    Data = 1,

    /// <summary>
    ///     Register
    /// </summary>
    Register = 3,

    /// <summary>
    ///     Extended register
    /// </summary>
    ExtendedRegister = 4,

    /// <summary>
    ///     Demand register
    /// </summary>
    DemandRegister = 5,

    /// <summary>
    ///     Profile generic
    /// </summary>
    ProfileGeneric = 7,

    /// <summary>
    ///     Clock
    /// </summary>
    Clock = 8,

    /// <summary>
    ///     Script table
    /// </summary>
    ScriptTable = 9,

    /// <summary>
    ///     Register activation
    /// </summary>
    RegisterActivation = 6,

    /// <summary>
    ///     Schedule
    /// </summary>
    Schedule = 10,

    /// <summary>
    ///     Special days table
    /// </summary>
    SpecialDaysTable = 11,

    /// <summary>
    ///     Association SN
    /// </summary>
    AssociationSn = 12,

    /// <summary>
    ///     Association LN
    /// </summary>
    AssociationLn = 15,

    /// <summary>
    ///     SAP Assignment
    /// </summary>
    SapAssignment = 17,

    /// <summary>
    ///     Image transfer
    /// </summary>
    ImageTransfer = 18,

    /// <summary>
    ///     IEC local port setup
    /// </summary>
    IecLocalPortSetup = 19,

    /// <summary>
    ///     Activity calendar
    /// </summary>
    ActivityCalendar = 20,

    /// <summary>
    ///     Register monitor
    /// </summary>
    RegisterMonitor = 21,

    /// <summary>
    ///     Single action schedule
    /// </summary>
    SingleActionSchedule = 22,

    /// <summary>
    ///     IEC HDLC setup
    /// </summary>
    IecHdlcSetup = 23,

    /// <summary>
    ///     IEC twisted pair (1) setup
    /// </summary>
    IecTwistedPair1Setup = 24,

    /// <summary>
    ///     M-BUS slave port setup
    /// </summary>
    MBusSlavePortSetup = 25,

    /// <summary>
    ///     Utility tables
    /// </summary>
    UtilityTables = 26,

    /// <summary>
    ///     Modem configuration / PSTN modem configuration
    /// </summary>
    ModemConfiguration = 27,

    /// <summary>
    ///     Auto answer
    /// </summary>
    AutoAnswer = 28,

    /// <summary>
    ///     Auto connect / PSTN Auto dial
    /// </summary>
    AutoConnect = 29,

    /// <summary>
    ///     Data protection
    /// </summary>
    DataProtection = 30,

    /// <summary>
    ///     Push setup
    /// </summary>
    PushSetup = 40,

    /// <summary>
    ///     TCP-UDP setup
    /// </summary>
    TcpUdpSetup = 41,

    /// <summary>
    ///     IPv4 setup
    /// </summary>
    Ipv4Setup = 42,

    /// <summary>
    ///     MAC address setup (Ethernet setup)
    /// </summary>
    MacAddressSetup = 43,

    /// <summary>
    ///     PPP setup
    /// </summary>
    PppSetup = 44,

    /// <summary>
    ///     GPRS modem setup
    /// </summary>
    GprsModemSetup = 45,

    /// <summary>
    ///     SMTP setup
    /// </summary>
    SmtpSetup = 46,

    /// <summary>
    ///     GSM diagnostic
    /// </summary>
    GsmDiagnostic = 47,

    /// <summary>
    ///     IPv6 setup
    /// </summary>
    Ipv6Setup = 48,

    /// <summary>
    ///     S-FSK Phy & MAC setup
    /// </summary>
    SFskPhyMacSetup = 50,

    /// <summary>
    ///     S-FSK Active initiator
    /// </summary>
    SFskActiveInitiator = 51,

    /// <summary>
    ///     S-FSK MAC synchronization timeouts
    /// </summary>
    SFskMacSynchronizationTimeouts = 52,

    /// <summary>
    ///     S-FSK MAC counters
    /// </summary>
    SFskMacCounters = 53,

    /// <summary>
    ///     IEC 61334-4-32 LLC setup
    /// </summary>
    Iec61334432LlcSetup = 55,

    /// <summary>
    ///     S-FSK IEC 61334-4-32 LLC setup
    /// </summary>
    SFskIec61334432LlcSetup = 56,

    /// <summary>
    ///     S-FSK Reporting system list
    /// </summary>
    SFskReportingSystemList = 57,

    /// <summary>
    ///     ISO/IEC 8802-2 LLC Type 1 setup
    /// </summary>
    IsoIec8802Type1Setup = 57,

    /// <summary>
    ///     ISO/IEC 8802-2 LLC Type 2 setup
    /// </summary>
    IsoIec88022LlcType2Setup = 58,

    /// <summary>
    ///     ISO/IEC 8802-2 LLC Type 3 setup
    /// </summary>
    IsoIec88022LlcType3Setup = 59,

    /// <summary>
    ///     Register table
    /// </summary>
    RegisterTable = 61,

    /// <summary>
    ///     Compact data
    /// </summary>
    CompactData = 62,

    /// <summary>
    ///     Status mapping
    /// </summary>
    StatusMapping = 63,

    /// <summary>
    ///     Security setup
    /// </summary>
    SecuritySetup = 64,

    /// <summary>
    ///     Parameter monitor
    /// </summary>
    ParameterMonitor = 65,

    /// <summary>
    ///     Sensor manager
    /// </summary>
    SensorManager = 67,

    /// <summary>
    ///     Arbitrator
    /// </summary>
    Arbitrator = 68,

    /// <summary>
    ///     Disconnect control
    /// </summary>
    DisconnectControl = 70,

    /// <summary>
    ///     Limiter
    /// </summary>
    Limiter = 71,

    /// <summary>
    ///     M-Bus client
    /// </summary>
    MBusClient = 72,

    /// <summary>
    ///     Wireless Mode Q channel
    /// </summary>
    WirelessModeQChannel = 73,

    /// <summary>
    ///     M-Bus master port setup
    /// </summary>
    MBusMasterPortSetup = 74,

    /// <summary>
    ///     DLMS/COSEM server M-Bus port setup
    /// </summary>
    DlmsCosemServerMBusPortSetup = 76,

    /// <summary>
    ///     M-Bus diagnostic
    /// </summary>
    MBusDiagnostic = 77,

    /// <summary>
    ///     61334-4-32 LLC SSCS setup
    /// </summary>
    Iec61334432LlcSscsSetup = 80,

    /// <summary>
    ///     PRIME NB OFDM PLC Physical layer counters
    /// </summary>
    PrimeNbOfdmPlcPhysicalLayerCounters = 81,

    /// <summary>
    ///     PRIME NB OFDM PLC MAC setup
    /// </summary>
    PrimeNbOfdmPlcMacSetup = 82,

    /// <summary>
    ///     PRIME NB OFDM PLC MAC functional parameters
    /// </summary>
    PrimeNbOfdmPlcMacFunctionalParameters = 83,

    /// <summary>
    ///     PRIME NB OFDM PLC MAC counters
    /// </summary>
    PrimeNbOfdmPlcMacCounters = 84,

    /// <summary>
    ///     PRIME NB OFDM PLC MAC network administration data
    /// </summary>
    PrimeNbOfdmPlcMacNetworkAdministrationData = 85,

    /// <summary>
    ///     PRIME NB OFDM PLC Application identification
    /// </summary>
    PrimeNbOfdmPlcApplicationIdentification = 86,

    /// <summary>
    ///     G3-PLC MAC layer counters
    /// </summary>
    G3PlcMacLayerCounters = 90,

    /// <summary>
    ///     G3 NB OFDM PLC MAC layer counters
    /// </summary>
    G3NbOfdmPlcMacLayerCounters = 90,

    /// <summary>
    ///     G3-PLC MAC setup
    /// </summary>
    G3PlcMacSetup = 91,

    /// <summary>
    ///     G3 NB OFDM PLC MAC setup
    /// </summary>
    G3NbOfdmPlcMacSetup = 91,

    /// <summary>
    ///     G3-PLC 6LoWPAN adaptation layer setup
    /// </summary>
    G3Plc6LoWpanAdaptationLayerSetup = 92,

    /// <summary>
    ///     G3 NB OFDM PLC 6LoWPAN adaptation layer setup
    /// </summary>
    G3NbOfdmPlc6LoWpanAdaptationLayerSetup = 92,

    /// <summary>
    ///     Wi-SUN setup
    /// </summary>
    WiSunSetup = 95,

    /// <summary>
    ///     Wi-SUN diagnostic
    /// </summary>
    WiSunDiagnostic = 96,

    /// <summary>
    ///     RPL diagnostic
    /// </summary>
    RplDiagnostic = 97,

    /// <summary>
    ///     MPL diagnostic
    /// </summary>
    MplDiagnostic = 98,

    /// <summary>
    ///     NTP Setup
    /// </summary>
    NtpSetup = 100,

    /// <summary>
    ///     ZigBee® SAS startup
    /// </summary>
    ZigbeeSasStartup = 101,

    /// <summary>
    ///     ZigBee® SAS join
    /// </summary>
    ZigbeeSasJoin = 102,

    /// <summary>
    ///     ZigBee® SAS APS fragmentation
    /// </summary>
    ZigbeeSasApsFragmentation = 103,

    /// <summary>
    ///     ZigBee® network control
    /// </summary>
    ZigbeeNetworkControl = 104,

    /// <summary>
    ///     ZigBee® tunnel setup
    /// </summary>
    ZigbeeTunnelSetup = 105,

    /// <summary>
    ///     Account
    /// </summary>
    Account = 111,

    /// <summary>
    ///     Credit
    /// </summary>
    Credit = 112,

    /// <summary>
    ///     Charge
    /// </summary>
    Charge = 113,

    /// <summary>
    ///     Token gateway
    /// </summary>
    TokenGateway = 115,

    /// <summary>
    ///     Function control
    /// </summary>
    FunctionControl = 122,

    /// <summary>
    ///     Array manager
    /// </summary>
    ArrayManager = 123,

    /// <summary>
    ///     Communication port protection
    /// </summary>
    CommunicationPortProtection = 124,

    /// <summary>
    ///     SCHC-LPWAN setup
    /// </summary>
    SchcLpwanSetup = 126,

    /// <summary>
    ///     SCHC-LPWAN diagnostic
    /// </summary>
    SchcLpwanDiagnostic = 127,

    /// <summary>
    ///     LoRaWAN setup
    /// </summary>
    LoRaWanSetup = 128,

    /// <summary>
    ///     LoRaWAN diagnostic
    /// </summary>
    LoRaWanDiagnostic = 129,

    /// <summary>
    ///     ISO/IEC14908 Identification
    /// </summary>
    IsoIec14908Identification = 130,

    /// <summary>
    ///     ISO/IEC 14908 Protocol setup
    /// </summary>
    IsoIec14908ProtocolSetup = 131,

    /// <summary>
    ///     ISO/IEC 14908 protocol status
    /// </summary>
    IsoIec14908ProtocolStatus = 132,

    /// <summary>
    ///     ISO/IEC 14908 diagnostic
    /// </summary>
    IsoIec14908Diagnostic = 133,

    /// <summary>
    ///     HS-PLC ISO/IEC 12139-1 MAC setup
    /// </summary>
    HsPlcIsoIec121391MacSetup = 140,

    /// <summary>
    ///     HS-PLC ISO/IEC 12139-1 CPAS setup
    /// </summary>
    HsPlcIsoIec121391CpasSetup = 141,

    /// <summary>
    ///     HS-PLC ISO/IEC 12139-1 IP SSAS setup
    /// </summary>
    HsPlcIsoIec121391IpSsasSetup = 142,

    /// <summary>
    ///     HS-PLC ISO/IEC 12139-1 HDLC SSAS setup
    /// </summary>
    HsPlcIsoIec121391HdlcSsasSetup = 143,

    /// <summary>
    ///     LTE monitoring
    /// </summary>
    LteMonitoring = 151
}