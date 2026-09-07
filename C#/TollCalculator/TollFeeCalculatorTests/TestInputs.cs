using System;
using System.Collections.Generic;
using System.Text;
using TollFeeCalculatorLibrary;

namespace TollFeeCalculatorTests;

internal class TestInputs
{
    public static IEnumerable<Vehicle> Vehicles()
    {
        var t = vechiles
            .Split("\r\n")
            .Select(x => new TestVehicle(x));

        return t;
    }

    public static IEnumerable<DateTime[]> DateTimes()
    {
        var s = dates
            .Split("\r\n");
          var t = s.Select(x => x.Split(','))
            .Select(x => x.Select(y => DateTime.Parse(y)).ToArray());

        return t;
    }

    const string vechiles = """
        Motorbike
        Tractor
        Emergency
        Diplomat
        Foreign
        Military
        `8VrAs6abmM<q
        +Pdrk_DWCM[w~
        [A )2&6;aR%Hr
        +\9&lf.-q8g1S
        [;U,]yv%Gy_hs
        ={r"ogmSTnoix
        R}ra9[4;t#j<f
        +-be'Uj(_6v*_
        lX{aXGY1W+7[^
        O(|l<kiU5MlAG
        \=RzxRx;|S$!F
        Agu>qq\uHG1Is
        P81ulHZD)m_5l
        \45&b&uOJ<>SI
        &/Cfq@r@k7'J_
        $p}_"Eb$HGQ7l
        ?PPeH8=x\;Saa
        GSJ;GXzG\N7P5
        iJ2ws 3"}=iJe
        'GsQiY;'|:0Fm
        u2Qj(r"x4Tadx
        t#v[?3Lc;5xyF
        !^%{;;a?6'r,K
        j(DRX[Ia^d}vc
        /{w^@7rF& `U\
        Kbb9?t'm/S2de
        0eSw(-qiOv|Bi
        ;UeN' >OJk7AJ
        \T'>2&' yfPw9
        K`sFqrE\KIYKy
        i.\\>9P}D`)y}
        W$D:ZE{j492IB
        ",bp0tCPc<N*X
        #\G*d1j>1i*yz
        -B#l&(24MI"-"
        *"~Y6{8Bj3wJ<
        LaTbzH"ZUByg#
        2b O;Rlx>l`sK
        ")/M4>"j7h.Ay
        W'v)=f/\*)ge<
        @aA29\9K9 Ej&
        [%]|UO.^;)RGV
        ewgBp%;2C3r'#
        8USY5oy"5~^L%
        zv?+k4dsR;;U!
        l$@la!Y\yg'$3
        uw"/H^"(}99]J
        6{i5#r\DcWmP@
        '4HmQkatrU(eS
        %C>)!J2n"w'"2
        i]:\"GXVjJ\H]
        Ky<igSNblNfo1
        {H36\u-y[}jrg
        'EIld8'>N'6XQ
        "N4s:"Y8*&&Su
        g<s@`\^!"uZC\
        L}lt[X[];:WMb
        [zLV1y_&o!*\{
        W~?qj(#@!E)\d
        WWu^rVG'uD"5r
        ,5|pe!_.O}'&y
        U&fpH>%\HK`mz
        $p7Q2#i%-AI-f
        Z;.bK&c/D^~01
        c`ha $43#}jzX
        9Y{[\+G8C<z!j
        !;oUM 5*fl/Sm
        BW2pEI-2I\G@N
        QQS5drj O^vuf
        G-%941F~$DF\3
        ;KC@t&`i-|!\O
        rFfQ{"i\:j\_f
        "[j:^"T6)ov"m
        "M=l[r.Z;%EXQ
        "2f+WVD@aL*g8
        K"kiU=t:V;z5:
        (!^G<^BmZg#,<
        gB@4Bt|wx&J)&
        vApu*Vrk$m,\J
        Nf@}&VjJsvPQ6
        WFq,}ZCzt\"%(
        BSh`~o\+vqb"r
        xf;}UTX|T0;;$
        =hez;{.";5;oY
        m\LW~\*ts;B!%
        B\NwZewsTM62"
        R\A"l1(r"{ Id
        KBH"PP)-nSKO:
        +("_A?U\lQy=V
        u`LIp"5hWd~d_
        :#<jDsQ^.*8=<
        ^JD%Odz &OX=i
        e#WP%&N3F0Z(W
        j*tR-|U4YuI]o
        C1&8k*+"`R%ZX
        z&f~e!/"1}>f|
        6-E#S-'~o))AB
        5~<"-y9Zsodp|
        "ITb3peAeCakL
         Dk>bDECf%k8i
        @6ILY^1i+.ElL
        >fI?m=VtKR>F`
        {x?'>)Y@?l\9F
        """;

    const string dates = """
        2026-09-07 10:00:00,2026-09-07 11:00:00,2026-09-07 11:01:00,2026-09-07 12:00:00
        2026-09-01 10:00:00,2026-09-01 11:00:00,2026-09-02 10:01:00,2026-09-03 15:00:00
        9587-07-28 23:05:23
        7240-07-13 02:00:40,9962-08-14 03:41:23
        0265-06-15 00:21:06,4496-11-19 21:33:16,5026-04-09 16:58:35
        9855-09-14 08:21:47,0829-11-28 04:13:07,4148-03-26 11:46:53,7888-12-02 10:03:58
        8744-11-13 21:29:59,2314-05-28 16:32:58,6142-07-26 14:01:22,4990-12-27 15:59:01,0445-03-24 04:39:34
        9018-08-19 19:54:43,2967-12-04 22:41:16,0893-01-21 17:40:25,3561-08-03 23:55:05,3338-07-05 03:53:43,9927-01-21 07:36:55
        3866-06-29 13:49:23,9743-06-02 22:23:25,1170-08-15 09:54:39,8781-11-19 18:25:51,5925-02-07 01:34:49,7369-12-05 01:32:36,8395-01-04 10:47:35
        3268-11-08 03:10:39,9925-06-03 23:01:36,7684-01-18 12:38:49,0629-08-21 01:00:40,3003-06-28 10:43:43,1673-10-03 07:45:36,5613-10-18 13:22:56,9426-04-03 02:45:01
        1414-05-02 04:46:58,1491-06-14 20:16:23,8514-03-08 08:17:20,3971-02-19 21:22:28,3525-11-27 06:02:23,6419-05-19 11:13:28,9868-01-28 07:58:59,1561-02-25 13:37:38,3150-05-21 06:28:21
        9002-05-19 14:29:05,4081-08-11 22:26:51,5853-09-10 01:20:05,9783-11-18 18:12:50,3984-03-11 12:40:40,1133-06-04 13:04:01,7411-09-09 08:10:42,3340-06-08 15:58:30,9450-04-14 12:58:01,8774-03-02 10:48:55
        0609-02-09 08:51:06,8084-06-11 01:11:17,4553-05-12 20:21:22,7387-01-14 05:58:17,0635-09-12 13:42:57,5223-12-16 19:22:16,2085-06-03 08:35:13,1349-12-19 02:18:25,1066-10-13 01:07:29,6133-02-13 08:56:29,2584-12-17 08:34:06
        6592-07-03 05:36:30,7827-04-24 11:16:43,6107-12-31 15:13:08,9862-09-20 13:43:34,6718-12-28 02:04:19,8034-04-22 15:54:32,9848-03-06 23:32:55,6460-05-29 08:43:53,5860-04-07 07:44:21,1965-03-07 20:25:00,5247-03-12 20:29:42,1045-01-10 00:33:36
        """;

}
